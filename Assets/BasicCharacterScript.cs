using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacterScript : MonoBehaviour
{

    bool hit;
    bool hitting;
    
    PlayerGod pgod;
    public int playerID;//determines which god to listen to. We'll be referencing this on all children.
    public Vector2 moveVector;
    public bool airborn; //Keeping attached to this guy since it's an almost purely control/movement related issue so it'll be dealt with almost exclusively on here.

    // public bool attacking;
    // public bool moving;
    bool running;
    bool turning;
    bool dashing;
    int dashDirection;//0 not dashing, 1 positive, 2 negative;
    public Vector3 traj;//I wonder why this is a vector3
    public int runTimer;
    public int runTarget; //how long it takes to start running
    public int lag;//lag frames. Specifically for grounded (basic)movement. Will still slide and do idle stuff.
    //public int movement;//will be updated by an array of options that's set to your buttons.
    //public int movementTimer; //will also be updated by an array.
    //public int movementTarget;
    //public int moveTotalTime;
    //public Ability[] Options;
    /// <summary> These are Wavedash/roll variables. Are incorporated in the old movement system to keep track of the initial directional inputs
    ///  public float startingVert;
    ///  public float startingHor;
    /// </summary>
    // public Vector3 attackVector;//starting hor and ver for attack but i did it in a vector this time cuz I'm not stupid;//THIS IS FOR THE MINE ATTACK, same as above.
    //public int hitTimer; //Only referenced
    public int facing; //-1 and 1 //Potentially underutisilzed now, cuz it alows for changing direction midair. Good for determinging orientation though. Maybe attacks reference this?
    public float baseSize;
    //variables to be fiddled with
    public float dashSpeed;
    public float dashAccel;
    public float runSpeed;
    public float fallSpeed;
    public float jumpForce;
    public float airAccel;
    public float airSpeed;
    public float airMax;
    public int landingLag; //specific landing lag (now going to be communicated by the animation (ie the animation/attack script will set the lag at certain instances for all situations)
    public int jumpLag; //soon to be antiquaited variable for the same reasons as above. This ISN'T jumpsquat.
    public float height; //displacement off the ground.
    public int[] inputs;//0 not pressed, 1 pressed. Simple as that.
    int[] holding = new int[4];   //this is for keeping track of up,down,left,right holding values. Used in the grounded movement function
    //public int attack;//int id of the current attack
    /// <summary> All of these will likely be moved to that animation script for easier shit.
    /// public GameObject[] attackObjects;
    /// public GameObject[] superObjects;
    /// public GameObject[] extraObjects;
    /// public int attackTimer;
    /// </summary>

    //public int invul; //if should be invul. Likely moving this to the Player1God script.
    ///Teleport Variables:
    ///private float xtarget;
    ///private float ytarget;
    int GetInput(float value)
    {
        if (Mathf.Abs(value) <= 0.66)
        {
            return 0;
        }
        else
        {
            return (int)(value / Mathf.Abs(value));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        pgod = gameObject.transform.parent.gameObject.GetComponent<PlayerGod>();
        playerID = pgod.playerID;
        baseSize = transform.localScale.x;
    }
    void UpdateVariables()
    {
        hit = pgod.hit;
        hitting = pgod.hitting;
        moveVector = pgod.moveVector;
    }
    void grounding()
    {
        if (transform.position.y + traj.y < height)
        {
            transform.position = new Vector3(transform.position.x, height, 0);
            airborn = false;
            traj = new Vector3(traj.x, 0, 0);
            lag = landingLag; //to be replaced
            //UPDATE THE CHARGES OF MULTIPLE AIR MOVES
        }
        if (transform.position.y > height)
        {
            airborn = true;
            resetRun();
        }
    }
    void resetRun()
    {
        running = false;
        dashing = false;
        turning = false;
    }
    void resetInputs()
    {
        if (GetInput(moveVector.x) == 1)
        {
            holding[2] += 1;
            holding[3] = 0;
        }
        else if (GetInput(moveVector.x) == -1)
        {
            holding[2] = 0;
            holding[3] += 1;
        }
        else
        {
            holding[2] = 0;
            holding[3] = 0;
        }
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] += -1;
        }

    }

    void AirStrafe()
    {
        if (airborn)
        {
            traj += new Vector3(0, -fallSpeed, 0);
                landingLag = jumpLag;
                if (Mathf.Abs(traj.x) > airMax)
                {
                    traj += new Vector3(traj.x / Mathf.Abs(traj.x) * airAccel * -1, 0, 0);
                }
                if (GetInput(moveVector.x) == 1)
                {
                    if (traj.x + airAccel < airSpeed)
                    {
                        traj += new Vector3(airAccel, 0, 0);
                    }
                    if (traj.x > airSpeed && traj.x < airSpeed + airAccel)
                    {
                        traj = new Vector3(airAccel, traj.y, 0);
                    }
                }
                if (GetInput(moveVector.x) == -1)
                {
                    if (traj.x - airAccel > -airSpeed)
                    {
                        traj += new Vector3(-airAccel, 0, 0);
                    }
                    if (traj.x < -airSpeed && traj.x > -airSpeed - airAccel)
                    {
                        traj = new Vector3(-airAccel, traj.y, 0);
                    }
                }
        }
        else //initial airborn velocity
        {
            if (Mathf.Abs(traj.x) < dashAccel * 3)
            {
                traj = new Vector3(0, traj.y, 0);
            }
            else
            {
                traj += new Vector3(-dashAccel * 3 * Mathf.Abs(traj.x) / traj.x, 0, 0);
            }
        }
    }
    void TurnAround()
    {
        if (GetInput(moveVector.x) == 1) //attacking failsafe required
        {
            transform.localScale = new Vector3(baseSize, baseSize, 1);
            facing = 1;
        }
        if (GetInput(moveVector.x) == -1)
        {
            facing = -1;
            transform.localScale = new Vector3(-baseSize, baseSize, 1);
        }
    } 
    void hitFunction()
    {
        //There's an example in the shapes fighting game, currently empty.
    }
    // Update is called once per frame
    void groundedMovement()
    {
        if (GetInput(moveVector.y) == 1)
        {
            if (dashing)
            {
                traj = new Vector3(runSpeed * dashDirection, 0, 0);
            }
            traj += new Vector3(0, jumpForce, 0);
            airborn = true;
        }
        else
        {
            if (turning)
            {
                traj += new Vector3(-dashAccel * dashDirection * 2, 0, 0);
                if (traj.x * dashDirection < -dashSpeed)
                {
                    dashDirection = dashDirection * -1;
                    turning = false;
                    running = true;
                }
            }
            else if (running)
            {
                if (GetInput(moveVector.x) == 0)
                {

                    traj = new Vector3(traj.x - dashAccel * dashDirection, traj.y, traj.z);
                }
                else
                {
                    if (GetInput(moveVector.x) == dashDirection)
                    {
                        if (Mathf.Abs(traj.x) <= runSpeed)
                        {
                            traj = new Vector3(traj.x + dashAccel * dashDirection, traj.y, traj.z);
                        }
                        if (Mathf.Abs(traj.x) > runSpeed)
                        {
                            traj = new Vector3(runSpeed * dashDirection, traj.y, traj.z);
                        }
                    }
                    else
                    {
                        turning = true;
                        running = false;
                    }
                }
                if (Mathf.Abs(traj.x) <= dashAccel * 3)
                {
                    running = false;
                }
            }
            else if (dashing)
            {
                if (dashDirection == -1 && GetInput(moveVector.x) == 1 && holding[3] <= 1)
                {
                    dashDirection = 1;
                    traj.x = GetInput(moveVector.x) * dashSpeed;
                    runTimer = 0;
                }
                if (dashDirection == 1 && GetInput(moveVector.x) == -1 && holding[2] <= 1)
                {
                    dashDirection = -1;
                    traj.x = GetInput(moveVector.x) * dashSpeed;
                    runTimer = 0;
                }
                if (Mathf.Abs(traj.x) <= dashAccel * 3)
                {
                    dashing = false;
                }
                //move stuff
                if (GetInput(moveVector.x) != 0)
                {
                    runTimer += 1;
                    if (Mathf.Abs(traj.x) <= runSpeed)
                    {
                        traj = new Vector3(traj.x + dashAccel * dashDirection, traj.y, traj.z);
                    }
                }
                else
                {
                    if (runTimer < 10)
                    {
                        runTimer += 1;
                    }
                    else
                    {
                        traj = new Vector3(traj.x - dashAccel * dashDirection * 3, traj.y, traj.z);
                    }
                }
                if (Mathf.Abs(traj.x) > runSpeed)
                {
                    if (Mathf.Abs(traj.x) - runSpeed <= dashAccel)
                    {
                        traj = new Vector3(dashDirection * runSpeed, traj.y, traj.z);
                    }
                    else
                    {
                        traj += new Vector3(-dashAccel * dashDirection, traj.y, 0);
                    }
                }
                if (runTimer > runTarget)
                {
                    running = true;
                    dashing = false;
                }
            }
            else //should exclusively be standing left
            {
                if (Mathf.Abs(traj.x) < dashAccel * 3)
                {
                    traj = new Vector3(0, traj.y, 0);
                }
                else
                {
                    traj += new Vector3(-dashAccel * 3 * Mathf.Abs(traj.x) / traj.x, 0, 0);
                }
                if (GetInput(moveVector.x) != 0)
                {
                    runTimer = 0;
                    dashing = true;
                    dashDirection = GetInput(moveVector.x);
                    traj.x = GetInput(moveVector.x) * dashSpeed;
                }
            }
        }
    }


    void FixedUpdate()
    {
        TurnAround();
        UpdateVariables();
        grounding();
        resetInputs();
        transform.position += traj;
        lag += -1;
        if (hit || hitting)
        {
            hitFunction();
        } 
        else if(lag >= 0)
        {
            //lag? To be replaced with animation?
        }
        else
        {
            if (airborn)
            {
                AirStrafe();
            }
            else
            {
                groundedMovement();
            }
        }
    }
}
