using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGroundMovement : MonoBehaviour
{
    PlayerInfo infoScript;
    ReceiveInputs inputs;
    public float dashSpeed;
    float dashAccel;
    float dashDecel;
    int turnFrames;
    float runSpeed;
    float jumpForce;
    public GameObject idleAnim;
    public GameObject dashAnim;
    public GameObject runAnim;
    public GameObject turnaroundAnim;
    public GameObject jumpSquatAnim;
    public GameObject reverseDashAnim;
    int dashDirection; //0 = not dashing;
    public bool dashing;
    bool running;
    bool turning;
    int runTimer;
    int runTarget;
    int runHold;
    float turnSpeed;
    GameObject godObject;
    // Start is called before the first frame update
    void OnEnable()
    {
        
        if (infoScript == null) 
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            godObject = dummy;
            infoScript = dummy.GetComponent<PlayerInfo>();
            inputs = infoScript.receiver;
            anim[] dashframes;
            dashframes = dashAnim.GetComponent<Options>().frames;
            foreach (anim a in dashframes)
            {
                runTarget += a.frames;
            }
            runTarget += -1;
            dashSpeed = infoScript.dashSpeed;
            dashAccel = infoScript.dashAccel;
            dashDecel = infoScript.dashDecel;
            runSpeed = infoScript.runSpeed;
            jumpForce = infoScript.jumpForce;
        }
        ActivateAnimation(idleAnim);
        running = false;
        dashing = false;
        turning = false;
        dashDirection = infoScript.facing;
    }
    void ActivateAnimation(GameObject g)
    {
        if (g == dashAnim)
        {
            if (infoScript.facing == dashDirection)
            {
                g = dashAnim;
            }
            else
            {
                g = reverseDashAnim;
            }
            infoScript.facing = dashDirection;
            Vector3 moveVector = new Vector3(infoScript.dashNumber - infoScript.dashSpeed, 0, 0);
            godObject.transform.position += new Vector3(infoScript.facing * moveVector.x, moveVector.y, 0);
            infoScript.transform.localScale = new Vector3(infoScript.facing, 1, 1);
        }
        if(g == turnaroundAnim)
        {
            infoScript.facing = dashDirection * -1;
            infoScript.transform.localScale = new Vector3(infoScript.facing, 1, 1);
        }
        if (g == infoScript.currentAnim)
        {
            g.GetComponent<Options>().currentAnim = 0;
            g.GetComponent<Options>().currentAnimFrame = 0;
            for (int i = 0; i < g.GetComponent<Options>().frames.Length; i++)
            {
                if (g.GetComponent<Options>().frames[i] == null)
                {
                    break;
                }
                g.GetComponent<Options>().frames[i].spriteObject.active = false;
            }
        }
        else
        {
            idleAnim.active = false;
            dashAnim.active = false;
            runAnim.active = false;
            turnaroundAnim.active = false;
            jumpSquatAnim.active = false;
            reverseDashAnim.active = false;
            g.active = true;
        }

    }
    void Deactivate()
    {
        if (idleAnim.active == false &&
        dashAnim.active == false &&
        runAnim.active == false &&
        turnaroundAnim.active == false && reverseDashAnim.active == false)
        {
            gameObject.active = false;
        }
    }
    void failSafeFall()
    {
        if (infoScript.GetComponent<OptionsReference>().flybackDefault.active == false)
        {
            if (godObject.transform.position.y != 0)
            {
                if (infoScript.traj.y > 0)
                {
                    infoScript.traj = new Vector3(infoScript.traj.x, 0, 0);
                }
                infoScript.traj += new Vector3(0, -infoScript.fallSpeed, 0);
            }
            else
            {
                infoScript.traj = new Vector3(infoScript.traj.x, 0, 0);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        failSafeFall();
        if (infoScript.hit == 0)
        {
            if (inputs.holdingJump == 2)
            {
                //to be replaced with jump squat
                ActivateAnimation(jumpSquatAnim);
                if (dashing)
                {
                    infoScript.traj = new Vector3(runSpeed * dashDirection, 0, 0);
                }
                if (turning)
                {
                    infoScript.traj = new Vector3(0.001f, 0, 0);
                }
                if (Mathf.Abs(infoScript.traj.x) > infoScript.airMax)
                {
                    infoScript.traj = new Vector3(infoScript.airMax * dashDirection, 0, 0);
                }
                jumpSquatAnim.GetComponent<jumpSquat>().vector = new Vector3(infoScript.traj.x, infoScript.jumpForce, 0);
                //infoScript.traj += new Vector3(0, jumpForce, 0);
            }
            else
            {
                if (turning)
                {
                    infoScript.facing = dashDirection * -1;
                    infoScript.traj += new Vector3(-dashDecel * dashDirection * 2, 0, 0); //slowCHANGE
                    if (infoScript.traj.x * dashDirection < -dashSpeed)
                    {
                        dashDirection = dashDirection * -1;
                        turning = false;
                        running = true;
                        ActivateAnimation(runAnim);
                    }
                }
                else if (running)
                {
                    if (Mathf.Abs(infoScript.traj.x) <= dashDecel * 3)//slowCHANGE
                    {
                        running = false;//stop
                        ActivateAnimation(idleAnim);
                    }
                    else
                    {
                        if (inputs.holding[2] == 0 && inputs.holding[3] == 0)//slowdown
                        {

                            infoScript.traj = new Vector3(infoScript.traj.x - dashDecel * dashDirection, infoScript.traj.y, infoScript.traj.z); //slowCHANGE
                        }
                        else
                        {
                            if (Mathf.Abs(infoScript.traj.x) > dashSpeed * 0.5f && false) //this is an experiment to see if canceling a run into a same direction dash feels good.
                            {
                                dashing = true;
                                ActivateAnimation(dashAnim);
                                if (inputs.holding[2] >= 1 && dashDirection == -1)
                                {
                                    runTimer = 0;
                                    runHold = 0;
                                    dashing = true;
                                    running = false;
                                    infoScript.traj.x = -1 * dashSpeed;
                                }
                                else if (inputs.holding[3] >= 1 && dashDirection == 1)
                                {
                                    runTimer = 0;
                                    runHold = 0;
                                    dashing = true;
                                    running = false;
                                    infoScript.traj.x = 1 * dashSpeed;
                                }
                                else//turn
                                {
                                    turning = true;
                                    ActivateAnimation(turnaroundAnim);
                                    running = false;
                                }
                            }
                            else
                            {
                                if ((inputs.holding[2] >= 1 && dashDirection == -1) || (inputs.holding[3] >= 1 && dashDirection == 1))//continue
                                {
                                    if (Mathf.Abs(infoScript.traj.x) <= runSpeed)
                                    {
                                        infoScript.traj = new Vector3(infoScript.traj.x + Mathf.Abs(dashAccel) * dashDirection, infoScript.traj.y, infoScript.traj.z);//fastCHANGE
                                    }
                                    if (Mathf.Abs(infoScript.traj.x) > runSpeed)
                                    {
                                        infoScript.traj = new Vector3(runSpeed * dashDirection, infoScript.traj.y, infoScript.traj.z);
                                    }
                                }
                                else//turn
                                {
                                    turning = true;
                                    ActivateAnimation(turnaroundAnim);

                                    running = false;
                                }
                            }
                        }
                    }
                }
                else if (dashing)
                {
                    //activate dashing!
                    float number = dashSpeed / runTarget; //the deceleration
                    runTimer += 1;

                    if (dashDirection == -1 && inputs.holding[3] == 2)
                    {
                        dashDirection = 1;
                        runTimer = 0;
                        runHold = 0;

                        ActivateAnimation(dashAnim);
                        infoScript.traj.x = dashDirection * dashSpeed;
                    }
                    if (dashDirection == 1 && inputs.holding[2] == 2)
                    {
                        dashDirection = -1;
                        runTimer = 0;
                        runHold = 0;

                        ActivateAnimation(dashAnim);
                        infoScript.traj.x = dashDirection * dashSpeed;
                    }
                    //move stuff
                    if (inputs.holding[2] != 0 || inputs.holding[3] != 0)//This is to make it so you speed up into your run
                    {
                        runHold += 1;
                        infoScript.traj = new Vector3(infoScript.traj.x + dashAccel * dashDirection, infoScript.traj.y, infoScript.traj.z);//fastCHANGE
                    }
                    else
                    {
                        infoScript.traj += new Vector3(number * -dashDirection, 0, 0);
                    }
                    //this is where I left off
                    if (dashAccel > 0)
                    {
                        if (Mathf.Abs(infoScript.traj.x) >= runSpeed)//caps the max speed.
                        {

                            infoScript.traj = new Vector3(dashDirection * runSpeed, infoScript.traj.y, infoScript.traj.z);

                        }
                    }
                    if (dashSpeed < runSpeed)
                    {
                        if (Mathf.Abs(infoScript.traj.x) <= dashSpeed * 0.7f)
                        {
                            infoScript.traj = new Vector3(dashDirection * dashSpeed * 0.7f, infoScript.traj.y, infoScript.traj.z);
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(infoScript.traj.x) <= runSpeed * 0.7f)
                        {
                            infoScript.traj = new Vector3(dashDirection * runSpeed * 0.7f, infoScript.traj.y, infoScript.traj.z);
                        }
                    }

                    if (runTimer >= runTarget)
                    {
                        if (dashSpeed < runSpeed)
                        {
                            if (Mathf.Abs(infoScript.traj.x) <= dashSpeed * 0.71f) // if you're at maximum slow dash, return to idle
                            {
                                dashing = false;
                                ActivateAnimation(idleAnim);
                            }
                            else
                            {
                                running = true;
                                dashing = false;
                                ActivateAnimation(runAnim);
                            }
                        }
                        else
                        {
                            if (Mathf.Abs(infoScript.traj.x) <= runSpeed * 0.71f) // if you're at maximum slow dash, return to idle
                            {
                                dashing = false;
                                ActivateAnimation(idleAnim);
                            }
                            else
                            {
                                running = true;
                                dashing = false;
                                ActivateAnimation(runAnim);
                            }
                        }
                    }
                }
                else //should exclusively be standing left
                {
                    //idle
                    if (Mathf.Abs(infoScript.traj.x) < infoScript.traction * 3)
                    {
                        infoScript.traj = new Vector3(0, infoScript.traj.y, 0);
                    }
                    else
                    {
                        infoScript.traj += new Vector3(-infoScript.traction * 3 * Mathf.Abs(infoScript.traj.x) / infoScript.traj.x, 0, 0);
                    }
                    if (inputs.holding[2] > 1)
                    {
                        runTimer = 0;
                        runHold = 0;
                        dashing = true;
                        dashDirection = -1;

                        ActivateAnimation(dashAnim);
                        infoScript.traj.x = -1 * dashSpeed;
                    }
                    if (inputs.holding[3] > 1)
                    {
                        runTimer = 0;
                        runHold = 0;
                        dashing = true;
                        dashDirection = 1;

                        ActivateAnimation(dashAnim);
                        infoScript.traj.x = 1 * dashSpeed;
                    }
                }
            }
        }
        
    }
    void Update()
    {
        Deactivate();
    }

}
