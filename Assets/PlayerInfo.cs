using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public bool nonFatal;
    public int cpuLevel;
    //[HideInInspector] 
    public int cpuCounter;//used by the CPU controller scripts to determine when to act again.
    public string characterID; //the name of the character.
    public GameObject enemyObject;//this will be set during the menus later, but for now, it's going to be set by hand.
    public PlayerInfo enemyScript;
    public GameObject defaultAnim;
    public int player;
    public int hit; /*
                     * 0, not hit, 
                     * 1 hitting, 
                     * 2 hit a super 
                     * -1 got hit. 
                     * -2 got hit by super
                     * -3 got hit by combo move
                     * -4 "Other" -Currently just used for Washington bite
                     * -5 "Other2" Also used in washington bite
                     * --#s can also be used to tell more information, but not necessary. 
                     100 == They've won! Do the post game menu*/
    //[HideInInspector]
    public bool blockSuper;//this prevents supers from being used when another super is out.
    public Vector3 knockbackAngle;
    [HideInInspector] public bool resolveHit;
    public Vector3 traj;
    //[HideInInspector] 
    public ReceiveInputs receiver;
    public int facing;
    public float width;
    public float defaultHeight;
    [HideInInspector]
    public float height;
    public float dashSpeed;
    public float dashAccel;
    public float dashDecel;
    public float traction;//how much x to remove by default;
    public float runSpeed;
    public float jumpForce;
    public float shortHopForce;
    public float doubleJumpForce;
    public float fallSpeed;
    public float airAccel;
    public float airSpeed;
    public float airMax;//maximum carry speed for dash
    public int maxHealth;
    public int health;
    public int superCost;
    public commandsList groundCPUCommands;
    public commandsList airCPUCommands;
    [HideInInspector] public int score;//inverse of health, but useful in the longrun
    //[HideInInspector] 
    public int superCharge;
    [HideInInspector] public int blockSuperAfterHit;
    public GameObject currentAnim;
    public AudioSource currentAudio;
    [HideInInspector]
    public int[] triggerChargesMax = new int[4];
    //[HideInInspector]
    /// <summary>
    /// [HideInInspector] 
    /// </summary>
    public int superTimer; //this is used to determine what phase of the super to enter, shit like that. eg, go from bullet to falling.
    [HideInInspector] public int otherTimer; //this is used to determine what phase of the super to enter, shit like that. eg, go from bullet to falling.
    [HideInInspector] public int otherTimer2;
    [HideInInspector] public int[] buttonChargesMax = new int[4];
    [HideInInspector] public int[] moveChargesMax = new int[4];
    [HideInInspector] public float dashNumber;
    [HideInInspector] public OptionsReference reference;
    [HideInInspector] public float movementBound;
   // [HideInInspector] 
    public bool zeroHealth;
    //[HideInInspector] 
    public int zeroHealthRate;
    //[HideInInspector] 
    public float zeroHealthCounter;

    [HideInInspector] public BetterCameraMovement cam;
    scoreKeeper sc;
    public GameObject defaultGroundCPUObject;
    public GameObject defaultAirCPUObject;
    public GameObject landingDust;
    public GameObject victoryAnim;
    public GameObject regularDeath;
    public GameObject nonFatalAnim;
    public GameObject gunshotFatality;
    public GameObject dustFatality;
    public GameObject headsplodeFatality;
    public GameObject splatFatality;
    public GameObject grabStun;
    public GameObject tumble;
    public GameObject superTumble;
    public GameObject superReady;
    //super vars
    Vector3 placeholderVector1;
    Vector3 funnyVect;//this is used to stop the funny getting lauunched post hit
    // Start is called before the first frame update
    void ZeroHealth()
    {
        zeroHealthCounter += (1 / Time.timeScale) * Time.deltaTime * 60;
        if (zeroHealth)
        {
            if (health > 0)
            {
                if (zeroHealthRate == 0)
                {
                    health = 0;
                    zeroHealthCounter = 0;
                }
                else
                {
                    if (zeroHealthCounter > zeroHealthRate)
                    {
                        zeroHealthCounter = 0;
                        health += -1;
                    }
                }
            }
            else
            {
                if(zeroHealthCounter > zeroHealthRate + 30)
                {
                    health = -1;
                }
            }
        }
    }
    void Start()
    {
        movementBound = 35;
        reference = GetComponent<OptionsReference>();
 
        
        
        
        if (player == 0) //if unnassigned (testing) set to player 1 by default.
        {
            player = 1;
        }
        if (receiver == null)
        {
            receiver = GameObject.Find("P" + player + "InputReceiver").GetComponent<ReceiveInputs>();
            cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        }
        if (player == 1)
        {
            if (characterID != "Oswald")//if you have to add someo
            {
                cam.p1 = gameObject;
            }
        }
        else
        {
            if (characterID != "Oswald")
            {
                cam.p2 = gameObject;
            }
        }
        defaultAnim.active = true;
        enemyScript = enemyObject.GetComponent<PlayerInfo>();
    }
    void fixEndingSpeedGlitch()//I cannot for the life of me find where we do killing so I'm just putting this here.
    {
        if(enemyScript.health < 0)
        {
            if (hit == 0)
            {
                traj = new Vector3(0, traj.y, 0);
                if (traj.y > 0)
                {
                    traj = new Vector3(0, 0, 0);
                }
            }
        }
    }
    void subSubRecharge(SubOption sub)
    {
        if (sub.option != null)
        {
            GameObject g;
            g = sub.option;

            if (g.GetComponent<Options>().charges != null)
            {
                if (g.GetComponent<Options>().dontUpdateChargesOnLanding == false && g.GetComponent<Options>().maxCharges != 0)//the !0 prevents stuff from fucking with the juju if they haven't been initialized yet
                {
                    g.GetComponent<Options>().charges = g.GetComponent<Options>().maxCharges;
                }
            }
        }
    }
    void BlockSuperAfterHit()
    {
        blockSuperAfterHit += -1;
        if(blockSuperAfterHit == 0)
        {
            blockSuper = false;
        }
        if(blockSuperAfterHit > 0)
        {
            blockSuper = true;
        }
    }
    void subRecharge(Option opt)
    {
        subSubRecharge(opt.Up);
        subSubRecharge(opt.Forward);
        subSubRecharge(opt.Down);
        subSubRecharge(opt.Neutral);
        subSubRecharge(opt.UpForward);
        subSubRecharge(opt.UpForward);
    }
    void recharge()
    {
        subRecharge(reference.groundJumpOptions);
        
        subRecharge(reference.airJumpOptions);
        
        subRecharge(reference.groundAttackOptions);
        subRecharge(reference.airAttackOptions);
        subRecharge(reference.groundSpecialOptions);
        subRecharge(reference.airSpecialOptions);
        subRecharge(reference.groundMovementOptions);
        subRecharge(reference.airMovementOptions);
        subRecharge(reference.groundSuperOptions);
        subRecharge(reference.airSuperOptions);
    }
    void SetRed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Vector4(1, 0, 0, sr.color.a);
    }
    void noAnimAtStartFailSafe()
    {
        if (hit == 0 && enemyScript.hit == 0)
        {
            if (currentAnim == null)
            {
                defaultAnim.active = true;
            }
            if (currentAnim.active == false)
            {
                currentAnim.active = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        ZeroHealth();
        if (characterID == enemyScript.characterID && player == 2)
        {
            SetRed();
        }

        if (name == "Oswald")
        {
            print(traj);
        }
    }
    void FixedUpdate()
    {
        if (name == "Oswald") {
            print(traj);
                }
        noAnimAtStartFailSafe();
        fixEndingSpeedGlitch();
        BlockSuperAfterHit();
        if(enemyScript.characterID == "JFK")
        {
            int num = Mathf.FloorToInt(superCost/2);
            if (superCharge < num)
            {
                superCharge = num;
            }
        }
        cpuCounter += -1;
        if (traj.y + transform.position.y < 0)
        {
            if (transform.position.y > 0.1 && hit == 0)//this is to prevent options that make you airborn from triggering on the ground
            {
                if (transform.position.x + traj.x != float.NaN)
                {
                    
                    Instantiate(landingDust, new Vector3(transform.position.x + traj.x, 0, 0), Quaternion.identity);
                }
            }
            traj = new Vector3(traj.x, 0, 0);
            transform.position = new Vector3(transform.position.x, 0, 0);
            recharge();
        }
        if (enemyScript.hit == 2 || hit == 2 || enemyScript.hit == -2 || hit == -2)
        {
            handleSuper();
        }
        if (enemyScript.hit == 4 || hit == 4 || enemyScript.hit == -4 || hit == -4)
        {
            handleOther();
        }
        if (enemyScript.hit == 5 || hit == 5 || enemyScript.hit == -5 || hit == -5)
        {
            handleOtherTwo();
        }
        if (hit != 1)//restrict for camera bounds
        {
            Vector3 dummyVect = traj;

            if (traj.x + transform.position.x > movementBound && hit != 2)
            {
                dummyVect = new Vector3(0, traj.y, 0);
                transform.position = new Vector3(movementBound, transform.position.y, 0);

            }

            if (traj.x + transform.position.x < -movementBound && hit != 2)
            {
                dummyVect = new Vector3(0, traj.y, 0);
                transform.position = new Vector3(-movementBound, transform.position.y, 0);
            }

            if (traj.y > 0 && traj.y + transform.position.y > cam.topBound - height && hit != 2)
            {
                //dummyVect = new Vector3(traj.x, 0, 0);
                //transform.position = new Vector3(transform.position.x, cam.topBound - height, 0);
                //UNCOMMENT THIS if you want to restrict movement off the top of the screen
            }
            transform.position += dummyVect;

        }
        if (resolveHit == true)
        {
            float xdir = -1 * (transform.position.x - enemyObject.transform.position.x) / Mathf.Abs(transform.position.x - enemyObject.transform.position.x) * -1;
            int intx = 1;
            if (xdir > 0)
            {
                intx = 1;
            }
            else
            {
                intx = -1;
            }
            PlayerInfo enemyScript = enemyObject.GetComponent<PlayerInfo>();
            if (hit < 0)
            {
                enemyScript.resolveHit = true; //this i
                GetComponent<OptionsReference>().flybackDefault.active = true;
                float number = 0;
                number = Mathf.Sqrt((fallSpeed * enemyScript.knockbackAngle.y * enemyScript.knockbackAngle.y) / (enemyScript.fallSpeed));
                traj = new Vector3(enemyScript.knockbackAngle.x * xdir, number, 0);
            }
            else
            {
                currentAnim.active = false;
                traj = new Vector3(knockbackAngle.x * xdir, knockbackAngle.y, 0);
                GetComponent<OptionsReference>().flybackDefault.active = true;
                facing = -intx;
                if (superCharge == superCost)
                {
                    Instantiate(superReady);
                }
            }
            resolveHit = false;
            blockSuperAfterHit = 45;
            hit = 0;
        }
    }
    //----------------------------------------------------------------SUPERS-------------------------------------------------------

    void handleSuper()//This will handle the supers when scripts are necessary.
    {
        if (hit == -2 && enemyScript.hit != 2)
        {
            if (characterID == "BoxerTeddy")
            {
                TeddySuper();
            }
        }
        if (hit == 1 || hit == -3) //this is for the Teddy counter specifically. Idk why or how it works, don't use this structure in the future
        {
            if (enemyScript.characterID == "BoxerTeddy")
            {
                TeddySuper();
            }
        }
        //Washington-------------
        if (characterID == "Washington" && enemyScript.hit == -2)
        {
            WashingtonSuperWashington();
        }
        if (enemyScript.characterID == "Washington" && hit == -2)
        {
            WashingtonSuperHit();
        }
        if (characterID == "JFK" && enemyScript.hit == -2)
        {
            JFKSuperJFK();
        }
        if (enemyScript.characterID == "JFK" && hit == -2)
        {
            JFKSuperHit();
        }
        if (characterID == "Obama TA" && enemyScript.hit == -2)
        {
            ObamaSuper();
        }
        if (enemyScript.characterID == "Obama TA" && hit == -2)
        {
            ObamaSuperHit();
        }
        superTimer += 1;
    }
    void ObamaSuper()
    {
    }
    void ObamaSuperHit()
    {
        if (nonFatal == false)
        {
            if (grabStun.active == false)
            {
                currentAnim.active = false;
                grabStun.active = true;
            }

            if (grabStun.GetComponentInChildren<setSprite>().alpha >= 0)//this is my incredibly lazy way of knowing when the animation is over
            {
                GetComponent<SpriteRenderer>().color = new Vector4(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, grabStun.GetComponentInChildren<setSprite>().alpha);
                if (superTimer == 0)
                {
                    placeholderVector1 = new Vector3(transform.position.x, enemyObject.transform.position.y, 0);
                    if (placeholderVector1.y <= 1)
                    {
                        placeholderVector1 = new Vector3(transform.position.x, 1.1f, 0);//this keeps it from hitting the ground which was causing grabbed to stop (because grabbed is air for some reason and I'm afraid to change it)
                    }
                }
                grabStun.active = true;
                if (currentAnim != grabStun)
                {
                    currentAnim = grabStun;
                }
                FaceEnemy();
                if (superTimer % 4 == 0)
                {
                    transform.position = new Vector3(placeholderVector1.x + Random.Range(-0.5f, 0.5f), placeholderVector1.y + Random.Range(-1f, 1f), 0);
                }
                grabStun.GetComponentInChildren<setSprite>().alpha += -0.02f;
            }
            else
            {
                gameObject.active = false;
            }
        }
        else
        {
            if (health > 0)
            {
                if (grabStun.active == false)
                {
                    if (transform.position.y < 1)
                    {
                        transform.position = new Vector3(transform.position.x, 1, 0);
                    }
                    currentAnim.active = false;
                    grabStun.active = true;
                }
            }
            else
            {
                print("trying");
                print(transform.position.y > 0);
                print(nonFatalAnim.active == false);
                if (transform.position.y > 0 && nonFatalAnim.active == false)
                {
                    print("really trying");
                    currentAnim.active = false;
                    nonFatalAnim.active = true;
                }
            }
        }
    }
    void JFKSuperJFK()
    {
        if (superTimer == 0)
        {
            currentAnim.active = false;
            victoryAnim.active = true;
        }
        if (enemyScript.nonFatal == false)
        {
            if (superTimer == 47)
            {
                GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);
            }
            if (superTimer == 67)
            {
                GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
            }
            if (superTimer == 70)
            {
                currentAnim.active = false;
                headsplodeFatality.active = true;
            }
            if (superTimer == 180)
            {
                hit = 100;
            }
        }
    }
    void JFKSuperHit()
    {
        if (nonFatal == false)
        {
            if (superTimer < 70)
            {
                currentAnim.active = false;
                grabStun.active = true;
                traj = new Vector3(0, 0, 0);
                transform.position = new Vector3(enemyObject.transform.position.x + 2 * enemyScript.facing, enemyObject.transform.position.y + 2.5f, 0);
            }
            FaceEnemy();
            if (superTimer == 47)
            {
                GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);

            }
            if (superTimer == 67)
            {
                GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);

            }
            if (superTimer == 70)
            {
                grabStun.active = false;
                headsplodeFatality.active = true;
            }
        }
        else
        {
            if (superTimer < 225)
            {
                currentAnim.active = false;
                grabStun.active = true;
                traj = new Vector3(0, 0, 0);
                transform.position = new Vector3(enemyObject.transform.position.x + 2 * enemyScript.facing, enemyObject.transform.position.y + 2.5f, 0);
            }
            if(superTimer == 225)
            {
                currentAnim.active = false;
                nonFatalAnim.active = true;
                //nonFatalAnim.GetComponent<activateEnemyVictoryPose>().enabled = false;
                cam.enabled = false;
            }
            FaceEnemy();
        }
    }
    void FaceEnemy()
    {
        int dummyint;
        float dummyFloat;
        dummyFloat = -1 * (Mathf.Abs(transform.position.x - enemyObject.transform.position.x) / (transform.position.x - enemyObject.transform.position.x));
        if (dummyFloat > 0)
        {
            dummyint = 1;
        }
        else
        {
            dummyint = -1;
        }
        transform.localScale = new Vector3(dummyint, 1, 1);
        facing = dummyint;
    }
    void WashingtonSuperWashington()
    {
        if(superTimer > 5 && superTimer < 500 && enemyObject.transform.position.y == 0)
        {
            superTimer = 500;
        }
        if (superTimer < 530)
        {
            //this is just to make everything else nice
        }
        else if (superTimer < 737)
        {
            currentAnim.active = true;
            float num = transform.position.y / 60f + 0.5f;
            transform.position += new Vector3(0, -num, 0);
            if (superTimer == 700)
            {
                if (enemyScript.nonFatal == false)
                {
                    enemyScript.currentAnim.active = false;
                    enemyScript.dustFatality.active = true;
                }
                else
                {
                    enemyScript.currentAnim.active = false;
                    enemyScript.nonFatalAnim.active = true;
                    //enemyScript.nonFatalAnim.GetComponent<activateEnemyVictoryPose>().enabled = false;
                }
            }
        }
        else if (superTimer < 1000)
        {
            float num = transform.position.y / 60f + 0.5f;
            transform.position += new Vector3(0, num, 0);
            if (transform.position.y > 19)
            {
                transform.position += new Vector3(-7 * facing,6,0);
                superTimer = 1000;
                currentAnim.active = false;
                reference.airDefault.active = true;
            }
        }
        else if(superTimer < 2000) {
            if (transform.position.y + traj.y <= 0)
            {
                currentAnim.active = false;
                transform.position = new Vector3(transform.position.x, 0, 0);
                victoryAnim.active = true;
                superTimer = 2001;
            }
        }
    }
    void WashingtonSuperHit()
    {
        if (superTimer == 1)
        {
            currentAnim.active = false;
            superTumble.active = true;
            traj = new Vector3(0, 1f, 0);
        }
        if (currentAnim != superTumble && superTimer <= 20)
        {
            currentAnim.active = false;
        }
    }
    void TeddySuper()
    {

        if (hit == 1)//this is code that affects the hitter.
        {
            if (nonFatal == false)
            {
                if (superTimer < 150)
                {
                    traj = new Vector3(0, 0, 0);
                }
                if (superTimer == 61)
                {
                    currentAnim.active = false;
                    gunshotFatality.active = true;
                    GameObject.Find("HitBoxSoundGenerator").GetComponent<HitBoxSoundGenerator>().soundID = "No Sound";
                    hit = -3;//this just communicates to the camera trackers what to do
                }
                FaceEnemy();
            }
            else
            {
                if (superTimer < 152)
                {
                    traj = new Vector3(0, 0, 0);
                }
                if(superTimer == 152)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                    enemyObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                    currentAnim.active = false;
                    regularDeath.GetComponent<setTrajOnFrame>().enabled = false;
                    regularDeath.GetComponent<deathSlowMo>().enabled = false;
                    regularDeath.active = true;
                    traj = new Vector3(-facing * 1f, 1f, 0); health = -1;
                    hit = -3;
                }
                if(superTimer == 153)
                {
                    traj = new Vector3(-facing * 1f, 1f, 0);
                }
                FaceEnemy();
            }
        }
        else if(hit != -3)
        {
            if (enemyScript.nonFatal == false)
            {
                if (superTimer < 180)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);
                    traj = new Vector3(0, 0, 0);
                }
                else
                {

                    if (hit == 1)
                    {
                        hit = 0;
                    }
                }
            }
            else
            {
                if (superTimer < 151)
                {
                    traj = new Vector3(0, 0, 0);
                }
                if (superTimer == 152)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                    enemyObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                    hit = 0;
                }
                FaceEnemy();
            }
        }

    }
    //----------------------------------------------------------------OTHERS-------------------------------------------------------

    void handleOther()//This will handle the other attacks when scripts are necessary.

    {
        if (hit == -4)
        {
            if (enemyScript.characterID == "Washington")
            {
                WashingtonBite();
            }
        }
        otherTimer += 1;
    }
    void handleOtherTwo()
    {
        if (enemyScript.characterID == "Washington" && hit == -5)
        {
            WashingtonChomp();
        }
        if (characterID == "Washington" && hit == 5)
        {
            WashingtonChomp();
        }
        otherTimer2++;
    }
    void WashingtonBite() //realistically this is washington "hold"
    {
        traj = new Vector3(0, 0, 0);
        transform.position = enemyObject.transform.position + new Vector3(0.8f * enemyScript.facing, 2.25f, 0);
        currentAnim.active = false;
        grabStun.active = true;
        float xdir = (transform.position.x - enemyObject.transform.position.x) / Mathf.Abs(transform.position.x - enemyObject.transform.position.x) * -1;
        int intx = 1;
        if (xdir > 0)
        {
            intx = 1;
        }
        else
        {
            intx = -1;
        }
        facing = intx;

        if (otherTimer >= 120)
        {

            hit = 0;
            currentAnim.active = false;
            GetComponent<OptionsReference>().flybackDefault.active = true;
            traj += new Vector3(0.3f * -facing, 0.4f, 0);
            enemyScript.hit = 0;
            enemyScript.currentAnim.active = false;
            enemyObject.transform.position += new Vector3(0, 0.001f, 0);
            enemyScript.GetComponent<OptionsReference>().flybackDefault.active = true;
            enemyScript.traj += new Vector3(0.3f * facing, 0.4f, 0);
        }
    }
    void WashingtonChomp()
    {
        if(hit == 5)
        {
            hit = 1;
        }
        if(hit == -5)
        {
            if(otherTimer2 == 0)
            {
                currentAnim.active = false;
                GetComponent<OptionsReference>().hitDefault.active = true;
            }
            if (otherTimer2 % 3 == 0)
            {
                traj = new Vector3(traj.x * -1, 0, 0);
            }
            otherTimer = 0;
            if(otherTimer2 == 30)
            {
                enemyScript.hit = 0;
                enemyScript.score += 1;
                enemyScript.superCharge += 1;
            }
            if (otherTimer2 == 31)
            {
                print("we grabbing");
                hit = -4;
                if (health != 0)
                {
                    health += -1;
                }
                currentAnim.active = false;
                grabStun.active = true;
                traj = new Vector3(0, 0, 0);
            }
        }
    }
}
