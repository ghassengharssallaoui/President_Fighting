using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraPlayerTracker : MonoBehaviour
{
    PlayerInfo info;
    public GameObject targetObject;
    public float maxAccel;
    public bool top;
    public bool left;
    public float minDisplacement;
    public float maxDisplacement;
    public float minHeight;
    public float headroom = 1;
    float accel;
    //public float criticalDistance; gonna just set this to a number of frames of speed.
    public Vector3 target;
    float xSpeed;
    float ySpeed;
    float xdir;
    float lastdir;
    float ydir;
    public int superTimer; //used for cutscenes. When > 1 +1 every frame;
    public int dontSnapCounter;//This will stop the "snapping" ie, camera forcing itself behind a player so they're not off screen while the timer is active. It'll still allow movement but no snapping. Think Washington wiff. ALSO do this tomorrow.
    BetterCameraMovement camScript;
    PlayerInfo hittingPlayer;
    public bool snapOnLoad;
    public bool dontFollow;
    float updateTimer;

    // Start is called before the first frame update
    void OnEnable()
    {
        snapOnLoad = true;
    }
    void Start()
    {
        camScript = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        if (left)
        {
            if (top)
            {
                camScript.points[0] = gameObject;
            }
            else
            {
                camScript.points[1] = gameObject;
            }
        }
        else
        {
            if (top)
            {
                camScript.points[2] = gameObject;
            }
            else
            {
                camScript.points[3] = gameObject;
            }
        }
    }
    void SetTarget()
    {
        camScript.ignoreBounds = false;
        float targetDir;
        if (left) {
            targetDir = 1;
            
            if (camScript.p1.transform.position.x > camScript.p2.transform.position.x)
            {
                targetObject = camScript.p1;
            }
            else
            {
                targetObject = camScript.p2;
            }
        }
        else
        {
            if (camScript.p1.transform.position.x > camScript.p2.transform.position.x)
            {
                targetObject = camScript.p2;
            }
            else
            {
                targetObject = camScript.p1;
            }
            targetDir = -1;
        }
        if (top && false)
        {
            if (camScript.p1.transform.position.y + camScript.p1.GetComponent<PlayerInfo>().height * 1 > camScript.p2.transform.position.y + camScript.p2.GetComponent<PlayerInfo>().height * 1)
            {
                targetObject = camScript.p1;
            }
            else
            {
                targetObject = camScript.p2;
            }
        }
        info = targetObject.GetComponent<PlayerInfo>();
        if (Mathf.Abs(info.facing - targetDir) > 0.1)
        {
            target = new Vector3(targetObject.transform.position.x + (info.width/2 + minDisplacement) * targetDir, + targetObject.transform.position.y, 0);
        }
        else
        {
            target = new Vector3(targetObject.transform.position.x + (info.width/2 + maxDisplacement) * targetDir, +targetObject.transform.position.y, 0);
        }
        if (top)
        {

            if(info.traj.y >= 0)
            {
                target = new Vector3(target.x, target.y + (info.height * headroom), target.z);
            }
            else
            {
                target = new Vector3(target.x, target.y + (info.height * headroom), target.z);
            }
            if (target.y < minHeight)
            {
                target = new Vector3(target.x, minHeight, target.z);
            }
        }
        else
        {
            target = new Vector3(target.x, target.y + camScript.bottomBound, target.z);
        }
    }
    void xSlowDown()
    {

    }
    void FollowDefault()
    {

            if (targetObject.GetComponent<PlayerInfo>().hit != -1 && targetObject.GetComponent<PlayerInfo>().hit != 1 && dontFollow == false)
            {
                if (transform.position.x != target.x)
                {
                    xdir = (transform.position.x - target.x) / Mathf.Abs(transform.position.x - target.x) * -1;
                }//*-1 is cuz I did the order wrong. hopefully this doesn't crash
                if (Mathf.Abs(xdir - lastdir) < 0.1f)
                {
                    lastdir = xdir;
                    accel = 0;
                }
                if (accel < maxAccel)
                {
                    accel += maxAccel / 5;
                }
                if (Mathf.Abs(xSpeed) < Mathf.Abs(info.traj.x) || Mathf.Abs(xSpeed) < info.dashSpeed)
                {
                    xSpeed += accel * xdir;
                }
                bool decellerate;
                decellerate = false;
                if (info.traj.x == 0 || true)//I don't know why this wouldn't always be true. Check this later if the code breaks
                {
                    decellerate = true;
                }
                else
                {
                    if (Mathf.Abs(info.traj.x / Mathf.Abs(info.traj.x) - xdir) < 0.001f)//this is a really gross way of saying "if the traj != direction
                    {
                        decellerate = true;
                    }
                }
                if (decellerate)
                {
                    if (xSpeed > 0)
                    {
                        while (transform.position.x + xSpeed > target.x && Mathf.Abs(xSpeed) > 0.0001f)
                        {
                            xSpeed = xSpeed / 2;
                        }
                    }
                    if (xSpeed < 0)
                    {
                        while (transform.position.x + xSpeed < target.x && Mathf.Abs(xSpeed) > 0.0001f)
                        {
                            xSpeed = xSpeed / 2;
                        }
                    }
                }
                if (Mathf.Abs(xSpeed) < 0.00002f)
                {
                    xSpeed = 0;
                }
                ydir = (transform.position.y - target.y + 0.001f) / Mathf.Abs(transform.position.y - target.y + 0.001f) * -1;
                if (Mathf.Abs(ySpeed) < Mathf.Abs(info.traj.y) * 1.1f || Mathf.Abs(ySpeed) < info.jumpForce)
                {
                    ySpeed += accel * ydir;
                }
                if (ySpeed > 0)
                {
                    while (transform.position.y + ySpeed > target.y && Mathf.Abs(ySpeed) > 0.0001f)
                    {
                        ySpeed = ySpeed / 2;
                    }
                }
                if (ySpeed < 0)
                {
                    while (transform.position.y + ySpeed < target.y && Mathf.Abs(ySpeed) > 0.0001f)
                    {
                        ySpeed = ySpeed / 2;
                    }
                }
                if (Mathf.Abs(ySpeed) < 0.0002f)
                {
                    ySpeed = 0;
                }
                transform.position = new Vector3(transform.position.x + xSpeed, transform.position.y + ySpeed, 0);
                if (dontSnapCounter <= 0)
                {
                    Snap();
                }
                dontSnapCounter += -1;
                if (transform.position.y > camScript.topBound)
                {
                    transform.position = new Vector3(transform.position.x, camScript.topBound, transform.position.z);
                }
                if (transform.position.x < camScript.leftBound)
                {
                    transform.position = new Vector3(camScript.leftBound, transform.position.y, transform.position.z);
                }
                if (transform.position.x > camScript.rightBound)
                {
                    transform.position = new Vector3(camScript.rightBound, transform.position.y, transform.position.z);
                }
            }
        
    }
    void Snap()
    {
        if (left)//I think this is the snapping code
        {
            if (transform.position.x < targetObject.transform.position.x - info.width / 2)
            {
                transform.position = new Vector3(targetObject.transform.position.x - info.width / 2, transform.position.y, 0);
            }
        }
        else
        {

            if (transform.position.x > targetObject.transform.position.x + info.width / 2)
            {
                transform.position = new Vector3(targetObject.transform.position.x + info.width / 2, transform.position.y, 0);
            }
        }
        if (top)
        {
            if (transform.position.y < targetObject.transform.position.y + info.height * headroom)
            {
                transform.position = new Vector3(transform.position.x, targetObject.transform.position.y + info.height * headroom, 0);
            }
        }
        else
        {
            if (transform.position.y > targetObject.transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, targetObject.transform.position.y + camScript.bottomBound, 0);
            }
        }
    }
    PlayerInfo checkSpecialTarget(PlayerInfo p1, PlayerInfo p2) //just so we're clear, this one determines what initiates a camera move. The next function is what to do.
    {
        if (p1.characterID == "BoxerTeddy")
        {
            if (p1.hit == 2 || p2.hit == -2)
            {
                dontFollow = true;
            }
            if(p2.hit == -3)
            {
                dontFollow = false;
            }
        }
        else if (p1.characterID == "Obama TA")
        {
            if (p1.GetComponent<PlayerInfo>().blockSuper == true && p1.GetComponent<PlayerInfo>().blockSuperAfterHit <= 0)
            {
                if(superTimer <= 0)
                {
                    superTimer = 1;
                }
                return p1;

            }
        }
        else if (p1.characterID == "Washington")
        {
            if (p1.hit == 2 && p2.hit == -2)
            {
                if (superTimer == 0)
                {
                    superTimer = 1;
                }
                return p1;
            }
            else if
            (p1.hit == 2)
            {
                dontSnapCounter = 30;
            }
        }
        else //This is the general hit (JFK and others)
        {
            if (p1.hit == 2 && p2.hit == -2)
            {
                if (superTimer == 0)
                {
                    superTimer = 1;
                }
                return p1;
            }
        }
        return null;
    }
    void SetSpecialTargets(PlayerInfo hittingInfo)
    {
        if (hittingInfo.characterID == "JFK")
        {
            camScript.ignoreZoom = true;
            dontSnapCounter = 10;
            if (superTimer < 106)
            {
                //camScript.ignoreBounds = true;
                if (top)
                {
                    target = camScript.p1.transform.position + new Vector3(1 * hittingInfo.facing, 13, 0);
                }
                else
                {
                    if (transform.position.y <= 4.5f)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                    }
                    target = camScript.p1.transform.position + new Vector3(1 * hittingInfo.facing, 4.5f, 0);
                }
            }
            else
            {
                dontSnapCounter = 0;
                SetTarget();
                camScript.ignoreZoom = false;
                camScript.minZoom = 15;

            }
            FollowDefault();
            if (superTimer % 2 == 0)
            {
                FollowDefault();
            }
        }
        if (hittingInfo.characterID == "Washington")
        {
            if (hittingInfo.player == 1)
            {
                target = camScript.p2.transform.position;
                targetObject = camScript.p2;
            }
            else
            {
                target = camScript.p1.transform.position;
                targetObject = camScript.p1;
            }
            Snap();
            if (superTimer < 500)
            {
                if (target.y == 0 && superTimer < 455 && superTimer > 30)
                {
                    superTimer = 455;
                }
                if (top)
                {
                    target = new Vector3(target.x, target.y + (info.height * headroom), target.z);
                    if (target.y < minHeight)
                    {
                        target = new Vector3(target.x, minHeight, target.z);
                    }
                }
                else
                {
                    target = new Vector3(target.x, target.y + camScript.bottomBound, target.z);
                }
            }
            else if (superTimer < 750)
            {
                if (top)
                {
                    target = new Vector3(target.x, 30, target.z);
                }
                else
                {
                    target = new Vector3(target.x, target.y + camScript.bottomBound, target.z);
                }
            }
            else
            {
                SetTarget();
            }
        }
        if (hittingInfo.characterID == "Obama TA")
        {
            dontSnapCounter = 3;
            if (superTimer < 180)
            {
                target = new Vector3(target.x, hittingPlayer.transform.position.y + 3.5f, target.z);
            }
            else
            {
                target = new Vector3(target.x, hittingPlayer.transform.position.y + 4.25f, target.z);
            }
            bool move = false;
            float facing = hittingPlayer.transform.localScale.x;
            if (facing > 0)
            {
                if (left)
                {
                    move = true;
                }
                else
                {
                    move = false;
                }
            }
            else
            {
                if (left)
                {
                    move = false;
                }
                else
                {
                    move = true;
                }
            }
            if (move)
            {
                if (superTimer > 80 && superTimer < 180)
                {
                    if (camScript.minZoom > 10)
                    {
                        camScript.minZoom += -0.4f;
                    }
                }
                if (superTimer == 80)
                {    
                    maxAccel = 0.2f;
                    if (facing > 0)
                    {
                        if (transform.position.x > hittingPlayer.transform.position.x + 19)
                        {
                            target = new Vector3(hittingPlayer.transform.position.x + 19, target.y, target.z);
                        }
                    }
                    else
                    {
                        if (transform.position.x < hittingPlayer.transform.position.x - 19)
                        {
                            target = new Vector3(hittingPlayer.transform.position.x - 19, target.y, target.z);

                        }
                    }
                }
                if (superTimer == 180)
                {
                    camScript.ignoreBounds = true;
                    if (facing > 0)
                    {
                        if (transform.position.x > hittingPlayer.transform.position.x + 8)
                        {
                            target = new Vector3(hittingPlayer.transform.position.x + 8, target.y, target.z);

                        }
                    }
                    else
                    {
                        if (transform.position.x < hittingPlayer.transform.position.x - 8)
                        {
                            target = new Vector3(hittingPlayer.transform.position.x - 8, target.y, target.z);
                        }
                    }

                }
                if (superTimer == 354)
                {
                    SetTarget();
                    transform.position = target;
                    maxAccel = 0.02f;
                    dontSnapCounter = -1;
                    camScript.minZoom = camScript.defaultZoom;
                    superTimer = 0;

                }
            }
            if (!move)//my dumbass way of doing the other trackers
            {
                if (superTimer > 80)
                {
                    maxAccel = 0.1f;
                    if (facing < 0)
                    {
                        
                            target = new Vector3(hittingPlayer.transform.position.x + 8, target.y, target.z);

                        
                    }
                    else
                    {
                        
                            target = new Vector3(hittingPlayer.transform.position.x - 8, target.y, target.z);
                    }
                }
                if (superTimer == 354)
                {
                    SetTarget();
                    transform.position = target;
                    maxAccel = 0.02f;
                    dontSnapCounter = -1;
                    camScript.minZoom = camScript.defaultZoom;
                    superTimer = 0;

                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        hittingPlayer = checkSpecialTarget(camScript.p1.GetComponent<PlayerInfo>(), camScript.p2.GetComponent<PlayerInfo>());
        if (hittingPlayer == null)
        {
            hittingPlayer = checkSpecialTarget(camScript.p2.GetComponent<PlayerInfo>(), camScript.p1.GetComponent<PlayerInfo>());
        }
        if (hittingPlayer == null)
        {
            if (Time.timeScale > 0.000001f)
            {
                SetTarget();
                FollowDefault();
            }
        }
        else
        {
            SetSpecialTargets(hittingPlayer);
            FollowDefault();
        }
        if (superTimer > 0)
        {
            superTimer += 1;
        }
    }
 void Update() { 
        if(Time.timeScale == 0.000001f&& Time.deltaTime < 0.00001f)
        {

                updateTimer += Time.deltaTime;
            if (Time.timeScale == 0.000001f)
            {
                if (updateTimer >= 0.000001f / 60f)
                {
                    updateTimer += -0.000001f / 60f;
                    FixedUpdate();
                }
            }
        }
        if (snapOnLoad)
        {
            if(camScript.p1 != null && camScript.p2 != null)
            {
                SetTarget();
                transform.position = target;
                superTimer = 0;
                snapOnLoad = false;
            }
        }
    }
}
