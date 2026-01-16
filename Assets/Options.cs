using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Options : MonoBehaviour //need to rename!!!!!!!
{
    public int charges;

    public Options[] sharedCharges;
    public bool dontUpdateChargesOnLanding;
    //[HideInInspector] 
    public int maxCharges;
    public GameObject[] Parents;
    public int actionableFrame;
    public bool lockOut;
    public bool air;
    public bool noGravity;
    public bool airStrafe;
    public bool unlockFacing;
    public float groundSlideMultiplier;//if 0 then we don't do it at all.
    public bool hit;
    public bool superReset;//like it says, do we resetsuper when this animation comes out.
    public Option AttackOptions;
    public Option SpecialOptions;
    public Option MovementOptions;
    public Option SuperOptions;
    public Option JumpOptions;

    public GameObject grounding;
    public bool transferGroundingFrames;
    public int groundingTransferAddition;
    public bool loop; //
    public GameObject next; //if null will go to idle in reference.
    public commandsList commands;
    //[HideInInspector] 
    public bool dontReset;
    //[HideInInspector]
    public int currentFrame; //total frame
    [HideInInspector]
    public int currentAnimFrame;
    [HideInInspector]
    public int currentAnim;
    [HideInInspector] bool variablesTransfered;
    public anim[] frames;
    public GameObject sfxPrefab; //I don't know what this does
    [HideInInspector] bool markedForDeath;//this is implimented for the dumb on disable still letting the script run
    SpriteRenderer sr;
    OptionsReference reference;
    ReceiveInputs inputs;
    GameObject godObject;
   public  PlayerInfo infoScript;
    public cpuOption[] moveOptionsRO = new cpuOption[500]; //read only for debugging
    public cpuOption[] attackOptionsRO = new cpuOption[500];
    //[HideInInspector]
    public bool chargesSet;
    public bool variablesOnly;
    public bigEnabler big;
    public bool dontBigReset;
    public int bigCounter;
    void BigDisable()
    {
        if (dontBigReset == false)
        {
            if (big.doEnable || big.fakeEnable)
            {
                print(name);
                if (bigCounter == 2)//the two and three is to stop it from happening frame one, which breaks the game.
                {
                    transform.root.gameObject.active = false;
                }
            }
        }
        if(bigCounter < 2)
        {
            bigCounter += 1;
        }
    }
    void TransferSubOptions(Option myOpt, Option refOpt)
    {
        if(myOpt.Neutral.option == null)
        {
            myOpt.Neutral = refOpt.Neutral;
        }
        if(myOpt.Forward.option == null)
        {
            myOpt.Forward = refOpt.Forward;
        }
        if(myOpt.Up.option == null)
        {
            myOpt.Up = refOpt.Up;
        }
        if(myOpt.Down.option == null)
        {
            myOpt.Down = refOpt.Down;
        }
        if(myOpt.DownForward.option == null)
        {
            myOpt.DownForward = refOpt.DownForward;
        }
        if(myOpt.UpForward.option == null)
        {
            myOpt.UpForward = refOpt.UpForward;
        }
    }
    // Start is called before the first frame update
    void handleCharges()
    {
        if (chargesSet == false)
        {
            if (charges <= 0)
            {
                charges = 1000;
            }
            maxCharges = charges;
            chargesSet = true;
        }
        charges += -1;
        if (sharedCharges.Length > 0)
        {
            foreach (Options o in sharedCharges)
            {
                if (o != GetComponent<Options>())
                {
                    if (o.chargesSet == false)
                    {
                        if (o.charges == 0)
                        {
                            o.charges = 1000;
                            o.maxCharges = o.charges;
                            o.chargesSet = true;
                        }
                        o.maxCharges = o.charges;
                        o.chargesSet = true;
                    }
                    o.charges += -1;
                }
            }
        }
    }
    void OnEnable()
    {
        big = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
        handleCharges();

        if (variablesOnly == false)
        {
            if (reference == null)
            {
                GameObject dummy;
                dummy = gameObject;
                while (dummy.GetComponent<OptionsReference>() == null)
                {
                    dummy = dummy.transform.parent.gameObject;
                }
                reference = dummy.GetComponent<OptionsReference>();
                dummy = gameObject;
                while (dummy.GetComponent<PlayerInfo>() == null)
                {
                    dummy = dummy.transform.parent.gameObject;
                }
                infoScript = dummy.GetComponent<PlayerInfo>();
                inputs = infoScript.receiver;
                godObject = dummy;
                //
                for (int i = 0; i < frames.Length; i++)
                {
                    if (frames[i].spriteObject == null)
                    {
                        break;
                    }
                    frames[i].sprite = frames[i].spriteObject.GetComponent<SpriteRenderer>().sprite;
                }
                
            }
            if (superReset)
            {
                infoScript.superCharge = 0;
            }
            if (unlockFacing == true)
            {
                if (inputs.holding[2] > 0)
                {
                    infoScript.facing = -1;
                }
                if (inputs.holding[3] > 0)
                {
                    infoScript.facing = 1;
                }
            }
            if (variablesTransfered == false)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (air)
                    {
                        
                        TransferSubOptions(JumpOptions, reference.airJumpOptions);
                        TransferSubOptions(AttackOptions, reference.airAttackOptions);
                        TransferSubOptions(SpecialOptions, reference.airSpecialOptions);
                        TransferSubOptions(MovementOptions, reference.airMovementOptions);
                        TransferSubOptions(SuperOptions, reference.airSuperOptions);
                    }
                    else
                    {
                        
                        TransferSubOptions(JumpOptions, reference.groundJumpOptions);
                        TransferSubOptions(AttackOptions, reference.groundAttackOptions);
                        TransferSubOptions(SpecialOptions, reference.groundSpecialOptions);
                        TransferSubOptions(MovementOptions, reference.groundMovementOptions);
                        TransferSubOptions(SuperOptions, reference.groundSuperOptions);
                    }
                }


            }
            currentAnimFrame = 0;
            currentAnim = 0;
            variablesTransfered = true;
            resetHurtBoxes();
            infoScript.currentAnim = gameObject;
            markedForDeath = false;
            if (dontReset == false)
            {
                currentFrame = 0;
                InitializeAnimation();
            }
            else
            {
                int dummyint = currentFrame;
                markedForDeath = true;//this prevents the animator from ACTUALLY animating. (preventing onFrame effects from activating all at once when grounding)
                for (int i = 0; i < currentFrame; i++)
                {
                    Animate(false);
                    currentAnimFrame += 1;
                }
                dontReset = false;
                markedForDeath = false;
                Animate(true);

            }
        } 
    }
    void resetHurtBoxes()
    {
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i] == null)
            {
                break;
            }
            frames[i].spriteObject.active = false;
        }
    }


    SubOption convertInput(Option opt)
    {
        if (inputs.holding[0] > 1)
        {
            if(inputs.holding[2] > 1 || inputs.holding[3] > 1)
            {
                return opt.UpForward;
            }
            else
            {
                return opt.Up;
            }
        }
        else if (inputs.holding[1] > 1)
        {
            if (inputs.holding[2] > 1 || inputs.holding[3] > 1)
            {
                return opt.DownForward;
            }
            else
            {
                return opt.Down;
            }
        }
        else
        {
            if (inputs.holding[2] > 1 || inputs.holding[3] > 1)
            {
                return opt.Forward;
            }
            else
            {
                return opt.Neutral;
            }
        }

    }
    bool Check(Option opt)
    {
        if (lockOut == true)
        {
            return false;

        }
        if (opt.lockOut == true)
        {

            return false;

        }
        SubOption sub = convertInput(opt);
        if(sub.lockOut == true)
        {

            return false;

        }
        GameObject g;
        if(sub.option == null)
        {

            return false;

        }
        else
        {
            g = sub.option;

        }
        if (currentFrame >= actionableFrame + sub.additionalActionableFrame && (g.GetComponent<Options>().charges > 0 || g.GetComponent<Options>().chargesSet == false))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void BigDeactivate(bool disregardHit = false) //is this even working?
    {
        if (hit == false || disregardHit)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i] == null)
                {
                    break;
                }
                frames[i].spriteObject.active = false;
            }
            if (Parents.Length > 0)
            {
                foreach (GameObject g in Parents)
                {
                    g.active = false;
                }
            }
            currentAnimFrame = -3;
            markedForDeath = true;
            gameObject.active = false;
        }
    }
    void InitializeAnimation() //this is pretty shit. But should stop two spawns on frame 0.
    {
        if (godObject.transform.position.y <= 0 && air && currentFrame > 0 && hit == false)
        {
            if (grounding != null)
            {
                if (transferGroundingFrames)
                {
                    grounding.GetComponent<Options>().currentAnimFrame = currentAnim + groundingTransferAddition;
                    grounding.GetComponent<Options>().dontReset = true;
                }
                grounding.active = true;
            }
            else
            {
                reference.landingDefault.active = true;
            }
            BigDeactivate();
        }
        else if (currentAnimFrame >= frames[currentAnim].frames)
        {
            frames[currentAnim].spriteObject.active = false;
            currentAnim += 1;
            if (frames.Length <= currentAnim)
            {
                if (loop)
                {
                    currentAnim = 0;
                }
                else
                {
                    if (next != null)
                    {
                        next.active = true;
                        currentAnim += -1;
                    }
                    else
                    {
                        if (air)
                        {
                            reference.airDefault.active = true;
                            currentAnim += -1;
                        }
                        else
                        {
                            reference.groundDefault.active = true;
                            currentAnim += -1;
                        }
                    }
                    BigDeactivate(true);
                }
                if (currentAnimFrame != -3)
                {
                    currentAnimFrame = 0;
                }
            }
            else if (frames[currentAnim].spriteObject == null)
            {
                if (loop)
                {
                    currentAnim = 0;

                }
                else
                {
                    if (next != null)
                    {
                        next.active = true;
                        currentAnim += -1;
                    }
                    else
                    {
                        if (air)
                        {
                            reference.airDefault.active = true;
                            currentAnim += -1;
                        }
                        else
                        {
                            reference.groundDefault.active = true;
                            currentAnim += -1;
                        }
                    }
                    BigDeactivate(true);
                }
            }
            if (currentAnimFrame != -3)
            {
                currentAnimFrame = 0;
            }
        }
        if (markedForDeath == false)
        {
            if (frames[currentAnim].spriteObject.active != true)
            {
                frames[currentAnim].spriteObject.active = true;
            }
        }
    }
    void Animate(bool makeObjects)
    {
        if (godObject.transform.position.y <= 0 && air && currentFrame > 0 && hit == false)
        {
            if (grounding != null)
            {
                if (transferGroundingFrames)
                {
                    grounding.GetComponent<Options>().currentFrame = currentFrame + groundingTransferAddition;
                    grounding.GetComponent<Options>().dontReset = true;
                }
                grounding.active = true;
            }
            else
            {
                reference.landingDefault.active = true;
            }
            BigDeactivate();
        }
        else if(currentAnimFrame >= frames[currentAnim].frames)
        {
            
            frames[currentAnim].spriteObject.active = false;
            currentAnim += 1;
            if (frames.Length <= currentAnim)
            {
                if (loop)
                {
                    currentAnim = 0;
                }
                else
                {
                    if(next != null)
                    {
                        next.active = true;
                        currentAnim += -1;
                    }
                    else
                    {
                        if (air)
                        {
                            reference.airDefault.active = true;
                            currentAnim += -1;
                        }
                        else
                        {
                            reference.groundDefault.active = true;
                            currentAnim += -1;
                        }
                    }
                    BigDeactivate(true);
                }
                if (currentAnimFrame != -3)
                {
                    currentAnimFrame = 0;
                }
            }
            else if (frames[currentAnim].spriteObject == null)
            {
                if (loop)
                {
                    currentAnim = 0;

                }
                else
                {
                    if (next != null)
                    {
                        next.active = true;
                        currentAnim += -1;
                    }
                    else
                    {
                        if (air)
                        {
                            reference.airDefault.active = true;
                            currentAnim += -1;
                        }
                        else
                        {
                            reference.groundDefault.active = true;
                            currentAnim += -1;
                        }
                    }
                    BigDeactivate(true);
                }
            }
            if (currentAnimFrame != -3)
            {
                currentAnimFrame = 0;
            }
        }
        if((currentAnimFrame == 0 || (currentAnimFrame == 1 && currentAnim == 0)) && makeObjects)//This fixes an issue where objects don't spawn on the first animation
        {
            if(frames[currentAnim].objects.Length > 0)
            {
                foreach(GameObject g in frames[currentAnim].objects)
                {
                    GameObject dummy;
                    dummy = Instantiate(g, new Vector3(godObject.transform.position.x, godObject.transform.position.y + g.transform.position.y, 0), Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
                    dummy.transform.position += new Vector3(g.transform.position.x * infoScript.facing, 0, 0);//The reason we do this second is so that (f
                    dummy.transform.localScale = new Vector3(dummy.transform.localScale.x * infoScript.facing, dummy.transform.localScale.y, dummy.transform.localScale.z);
                    if (dummy.GetComponent<findPlayer>() != null)
                    {
                        dummy.GetComponent<findPlayer>().player = infoScript;
                    }
                }
            }
            if (frames[currentAnim].sfx.Length > 0)
            {
                for (int i = 0; i < frames[currentAnim].sfx.Length; i++)
                {
                    GameObject dummy;
                    dummy = Instantiate(sfxPrefab);
                    dummy.GetComponent<AudioSource>().clip = frames[currentAnim].sfx[i].sfx;
                    dummy.GetComponent<AudioSource>().Play();
                    dummy.GetComponent<sfxScript>().soundID = frames[currentAnim].sfx[i].soundID;
                    dummy.GetComponent<sfxScript>().info = infoScript;
                }
            }
        }
        if (markedForDeath == false)
        {
            if (frames[currentAnim].spriteObject.active != true)
            {
                frames[currentAnim].spriteObject.active = true;
            }
        }
        
    }
    
    void GroundSlide()
    {
        if (Mathf.Abs(infoScript.traj.x) < infoScript.traction * 3)
        {
            if (groundSlideMultiplier != 0)
            {
                infoScript.traj = new Vector3(0, infoScript.traj.y, 0);
            }
        }
        else
        {
            infoScript.traj += new Vector3(-infoScript.traction * groundSlideMultiplier * 3 * Mathf.Abs(infoScript.traj.x) / infoScript.traj.x, 0, 0);
        }
    }
    void AirStrafe()
    {

        if (inputs.holding[3] > 1)
        {
            if (infoScript.traj.x + infoScript.airAccel < infoScript.airSpeed)
            {
                infoScript.traj += new Vector3(infoScript.airAccel, 0, 0);
            }
            else
            {
                if (infoScript.traj.x < infoScript.airAccel + infoScript.airSpeed)//this makes sure you don't get cucked by super fast movement, and only round when you're close;
                {
                    infoScript.traj = new Vector3(infoScript.airSpeed, infoScript.traj.y, 0);
                }
            }
        }
        else if (inputs.holding[2] > 1)
        {
            if (infoScript.traj.x - infoScript.airAccel > -infoScript.airSpeed)
            {
                infoScript.traj += new Vector3(-infoScript.airAccel, 0, 0);
            }
            else
            {
                if (infoScript.traj.x > -infoScript.airAccel + infoScript.airSpeed)//this makes sure you don't get cucked by super fast movement, and only round when you're close;
                {
                    infoScript.traj = new Vector3(-infoScript.airSpeed, infoScript.traj.y, 0);
                }
            }
        }


    }
    void CPU()
    {
       //Aight, this is gonna be scuffed as fuck but here goes.
       //CPU options have a "movement" bool that determines order in a seperate array.
       //the array building process will ignore duplicate options
       //the options can also require/not require certain animations (specifically for stuff like dash dance)
       //before choosing an option, the highest percentage option is judged against a roll, if it wins, we use the attack options. If it loses, we use movement
       //That's it.
       //Every option has an expected animation that if not reached, the option aborts.
       //we trigger the options by increasing their "timer" to 1.
       //ALSO there's a chance, based on level, that it'll pick the first option in the attack/movement array, reserved for that option.
       if(infoScript.cpuCounter < 0) //used to have if lockout == false. Idk what it was for though. I removed it to try and get Washingtons teeth to work
       {
            
            cpuOption[] moveOptions = new cpuOption[500];
            cpuOption[] attackOptions = new cpuOption[500];
            int attackInt = 1;
            int moveInt = 1;
            float moveMax = 0;
            float attackMax = 0;//used for the all important check;
            Vector3 target;
            cpuOption[] commandParent;
            if(commands != null)
            {
                commandParent = commands.commands;
            }
            else
            {
                if (air)
                {
                    commandParent = infoScript.airCPUCommands.commands;
                }
                else
                {
                    commandParent = infoScript.groundCPUCommands.commands;
                }
            }
            for(int i = 0; i < commandParent.Length; i++)
            {
                if (commandParent[i] != null)
                {
                    if (commandParent[i].collision)
                    {
                        if (commandParent[i].move)
                        {
                            int add = commandParent[i].strength;
                            for (int g = 0; g < moveOptions.Length; g++)
                            {
                                if(moveOptions[g]!= null)
                                {
                                    if(commandParent[i] == moveOptions[g])
                                    {
                                        add += -1; 
                                    }
                                }
                            }

                            if (commandParent[i].commands[0].moveVector.x > 0 || (godObject.transform.position.x < 34 && godObject.transform.position.x > -34)) //this is the "don't walk into the wall" code
                            {
                                for (int l = 0; l < add; l++)
                                {
                                    moveOptions[moveInt] = commandParent[i];
                                    moveInt += 1;
                                }

                                if (commandParent[i].strength > moveMax)
                                {
                                    moveOptions[0] = commandParent[i];
                                    moveMax = commandParent[i].strength;
                                }
                            }
                        }
                        else
                        {
                            int add = commandParent[i].strength;
                            for (int g = 0; g < attackOptions.Length; g++)
                            {
                                if (attackOptions[g] != null)
                                {
                                    if (commandParent[i] == attackOptions[g])
                                    {
                                        add += -1;
                                    }
                                }
                            }

                            for (int l = 0; l < add; l++)
                            {
                                attackOptions[attackInt] = commandParent[i];
                                attackInt += 1;
                            }
                            
                            if (commandParent[i].strength > attackMax)
                            {
                                attackOptions[0] = commandParent[i];
                                attackMax = commandParent[i].strength;
                            }
                        }
                    }
                }
            }
            //need to figure out a way for other things to be dynamically added.
            //let's assume the size is infinite
            int rand = 0;
            if (Random.Range(1, 11) > attackMax && moveOptions[0] != null) //removing the whiles for testing
            {
                if (Random.Range(0, 20) < infoScript.cpuLevel)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, moveOptions.Length);
                }
                while (moveOptions[rand] == null)
                {
                    rand = Random.Range(0, moveOptions.Length);
                }
                moveOptions[rand].timer = 0;
            }
            else if (attackOptions[0] != null)//removing the whiles for testing
            {
                if (Random.Range(0, 20) < infoScript.cpuLevel)
                {
                    rand = 0;
                }
                else
                {
                    rand = Random.Range(0, attackOptions.Length);
                }
                while (attackOptions[rand] == null)
                {
                    rand = Random.Range(0, attackOptions.Length);
                }
                attackOptions[rand].timer = 0;
            }
            moveOptionsRO = moveOptions;
            attackOptionsRO = attackOptions;
            //when you get back to this. We need to figure out why the move/attackoption sub arrays aren't existing (presuming that's why the whiles were making it crash
        }
    }
    void BReverse()
    {
        if (inputs.holding[2] == 0 && inputs.holding[3] == 0 && Mathf.Abs(inputs.lastLeft) <= 10)
        {
            infoScript.facing = Mathf.Abs(inputs.lastLeft)/inputs.lastLeft;
        }
    }
    void handleOptionChange()
    {
        if (inputs != null)//oswald solution
        {

            if (inputs.holdingAttack == 2)
            {
                if (Check(AttackOptions))
                {
                    GameObject g = convertInput(AttackOptions).option;
                    if (g != gameObject)
                    {
                        BReverse();
                        inputs.holdingAttack += 1;
                        g.active = true;
                        currentFrame = -1;
                        BigDeactivate();
                    }

                }
            }
            if (inputs.holdingJump == 2)
            {
                if (Check(JumpOptions))
                {
                    GameObject g = convertInput(JumpOptions).option;
                    if (g != gameObject)
                    {
                        inputs.holdingJump += 1;
                        g.active = true;
                        currentFrame = -1;
                        BigDeactivate();
                    }

                }
            }
            if (inputs.holdingMovement == 2)
            {
                if (Check(MovementOptions))
                {
                    GameObject g = convertInput(MovementOptions).option;
                    if (g != gameObject)
                    {
                        BReverse();
                        inputs.holdingMovement += 1;
                        g.active = true;
                        currentFrame = -1;
                        BigDeactivate();
                    }

                }
            }
            if (inputs.holdingSpecial == 2)
            {
                if (Check(SpecialOptions))
                {
                    GameObject g = convertInput(SpecialOptions).option;
                    if (g != gameObject)
                    {
                        BReverse();
                        inputs.holdingSpecial += 1;
                        g.active = true;
                        currentFrame = -1;
                        BigDeactivate();
                    }

                }
            }
            if (inputs.holdingSuper == 2)
            {
                if (Check(SuperOptions) && infoScript.superCharge >= infoScript.superCost && infoScript.enemyScript.blockSuper == false)
                {
                    GameObject g = convertInput(SuperOptions).option;
                    if (g != gameObject)
                    {
                        inputs.holdingSuper += 1;
                        g.active = true;
                        currentFrame = -1;
                        BigDeactivate();
                    }

                }
            }
        }
    }
    void FixedUpdate()
    {
        BigDisable();
        if (variablesOnly == false)
        {
            if (infoScript.cpuLevel > 0)
            {
                CPU();
            }
            if ((infoScript.hit != 1 && infoScript.hit != -1) || hit)
            {
                if (currentFrame == 0 && currentAnimFrame != 0)
                {
                    resetHurtBoxes();
                }
                if (currentAnimFrame != 0)//this is because the "initialize animation function" is already doing it on the first frame. This prevents shit from enabling twice
                {
                    Animate(true);
                }
                currentFrame += 1;
                currentAnimFrame += 1;
                if (markedForDeath == false) //this stops things like grounded or next from interfering with cancels
                {
                    if (air)
                    {
                        if (airStrafe)
                        {
                            AirStrafe();
                        }
                        if (noGravity == false)
                        {
                            infoScript.traj += new Vector3(0, -infoScript.fallSpeed, 0);

                        }
                    }
                    else
                    {
                        GroundSlide();
                    }
                    handleOptionChange();

                }
            }
            //if currentframe == end, finish or loop
            //if ground, ground.

            else //HIT
            {
                if (infoScript.hit == -1)
                {
                    if (infoScript.enemyScript.hit == 1)
                    {
                        if (infoScript.health >= 1)
                        {
                            reference.hitDefault.active = true;
                        }
                        else
                        {
                            infoScript.regularDeath.active = true;//this will be DEATH once I make the death script
                        }
                        infoScript.health += -1;
                        infoScript.enemyScript.score += 1;
                        infoScript.enemyScript.superCharge += 1;
                        BigDeactivate();
                    }
                    else
                    {
                        //what to do when a super or combo move hits. In short, who knows!
                    }
                }
                if (infoScript.hit == 1)
                {
                    //do the stuff for hitting
                }

            }
        }

    }

}
        