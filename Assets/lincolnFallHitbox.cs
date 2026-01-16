using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lincolnFallHitbox : MonoBehaviour
{
    public hurtboxManager hm;
    public PlayerInfo info;
    public hitbox hitboxScript;
    bool noHurtboxesLastRound;
    bool keepOnCrushing;
    Vector3 headpoint;
    Vector3 rightpoint;
    Vector3 leftpoint;
    // Start is called before the first frame update
    void OnEnable()
    {
        hm = GameObject.Find("HurtboxManager").GetComponent<hurtboxManager>();
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        info = dummy.GetComponent<PlayerInfo>();
        hitboxScript = GetComponent<hitbox>();
        noHurtboxesLastRound = false;
    }
    void Push()
    {
        if (info.enemyScript.transform.position.y - info.enemyScript.traj.y > transform.position.y + transform.localScale.y / 2)
        {
            info.enemyScript.transform.position = new Vector3(info.enemyScript.transform.position.x, transform.position.y + transform.localScale.y / 2, 0);
            info.enemyScript.traj = new Vector3(info.enemyScript.traj.x, 0, 0);

        }
        else
        {
            if (info.enemyScript.transform.position.x > transform.position.x)
            {
                float xpos;
                xpos = transform.position.x + transform.localScale.x / 2;
                if (xpos > 35)//this puts it on top
                {
                    xpos = 35;
                    info.enemyScript.transform.position = new Vector3(35, transform.position.y + transform.localScale.y / 2, 0);
                    if (info.enemyScript.currentAnim.GetComponent<Options>() != null)
                    {
                        if (info.enemyScript.currentAnim.GetComponent<Options>().air == false)
                        {
                            info.enemyScript.currentAnim.active = false;
                            info.enemyScript.GetComponent<OptionsReference>().airDefault.active = true;
                        }
                    }
                }
                else
                {
                    info.enemyScript.transform.position = new Vector3(xpos +2f, info.enemyScript.transform.position.y, 0);
                }
                if(info.enemyScript.traj.x < 0)
                {
                    info.enemyScript.traj = new Vector3(0, info.enemyScript.traj.y, 0);

                }
            }
            else
            {
                float xpos;
                xpos = transform.position.x - transform.localScale.x / 2;
                if (xpos < -35)//this puts it on top
                {
                    info.enemyScript.transform.position = new Vector3(-35, transform.position.y + transform.localScale.y / 2, 0);
                    if (info.enemyScript.currentAnim.GetComponent<Options>() != null)
                    {
                        if (info.enemyScript.currentAnim.GetComponent<Options>().air == false)
                        {
                            info.enemyScript.currentAnim.active = false;
                            info.enemyScript.GetComponent<OptionsReference>().airDefault.active = true;
                        }
                    }
                }
                else
                {
                    info.enemyScript.transform.position = new Vector3(xpos - 2f, info.enemyScript.transform.position.y, 0);
                }
                if (info.enemyScript.traj.x > 0)
                {
                    info.enemyScript.traj = new Vector3(0, info.enemyScript.traj.y, 0);

                }
            }
        }
        
    }
    void Crush()
    {
        float height = headpoint.y - info.enemyScript.transform.position.y;
        float ypos;
        ypos = info.transform.position.y - height;
        if (ypos > 0)
        {
            info.enemyScript.transform.position = new Vector3(info.enemyScript.transform.position.x, ypos, 0);
        }
        else
        {
            keepOnCrushing = true;
            info.enemyScript.currentAnim.active = false;
            info.enemyScript.health = -1;
            info.enemyScript.transform.position = new Vector3(info.enemyScript.transform.position.x,0.001f,0);
            if (info.enemyScript.nonFatal)
            {
                info.enemyScript.nonFatalAnim.active = true;
                info.enemyScript.hit = -2;
            }
            else
            {

                info.enemyScript.splatFatality.active = true;
            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (keepOnCrushing)
        {
            if (info.enemyScript.nonFatal)
            {
                if (info.enemyScript.currentAnim != info.enemyScript.nonFatalAnim)
                {
                    info.enemyScript.currentAnim.active = false;
                    info.enemyScript.nonFatalAnim.active = true;
                    info.enemyScript.hit = -2;
                }
            }
            else
            {
                if (info.enemyScript.currentAnim != info.enemyScript.splatFatality)
                {
                    info.enemyScript.currentAnim.active = false;
                    info.enemyScript.splatFatality.active = true;
                }
            }
        }
        headpoint = new Vector3(0, 0, -100);
        rightpoint = new Vector3(-100, 0, 0);
        leftpoint = new Vector3(100, 0,0);
        if(info.player == 1)
        {
            for(int i = 0; i < hm.p2hurtboxes.Length; i++)
            {
                if (hm.p2hurtboxes[i] != null)
                {
                    float height = hm.p2hurtboxes[i].transform.position.y + hm.p2hurtboxes[i].transform.localScale.y;
                    float rightArm = hm.p2hurtboxes[i].transform.position.x + hm.p2hurtboxes[i].transform.localScale.x/2;
                    float leftArm = hm.p2hurtboxes[i].transform.position.x + hm.p2hurtboxes[i].transform.localScale.x/2;
                    if (height > headpoint.y)
                    {
                        headpoint = new Vector3(hm.p2hurtboxes[i].transform.position.x, height, 0); 
                    }
                    if (rightArm > rightpoint.x)
                    {
                        rightpoint = new Vector3(rightArm, 0, 0);
                    }
                    if (leftArm < leftpoint.x)
                    {
                        leftpoint = new Vector3(leftArm, 0, 0);
                    }
                }
            }
        }
        else 
        {
            for (int i = 0; i < hm.p1hurtboxes.Length; i++)
            {
                if (hm.p1hurtboxes[i] != null)
                {
                    float height = hm.p1hurtboxes[i].transform.position.y + hm.p1hurtboxes[i].transform.localScale.y;
                    float rightArm = hm.p1hurtboxes[i].transform.position.x + hm.p1hurtboxes[i].transform.localScale.x / 2;
                    float leftArm = hm.p1hurtboxes[i].transform.position.x - hm.p1hurtboxes[i].transform.localScale.x / 2;
                    if (height > headpoint.y)
                    {
                        headpoint = new Vector3(hm.p1hurtboxes[i].transform.position.x, height, 0);
                    }
                    if (rightArm > rightpoint.x)
                    {
                        rightpoint = new Vector3(rightArm, 0, 0);
                    }
                    if (leftArm < leftpoint.x)
                    {
                        leftpoint = new Vector3(leftArm, 0, 0);
                    }
                }
            }
        }
        bool collide = false;
        for(int i = 0; i < hitboxScript.hurtboxesHit.Length; i++)
        {
            if (hitboxScript.hurtboxesHit[i] != null)
            {
                if(hitboxScript.hurtboxesHit[i].type == "")
                {
                    collide = true;
                }
            }
        }
        if (collide)
        {
            if(info.enemyScript.transform.position.y > info.transform.position.y)
            {
                
                Push();
            }
            else if(info.enemyScript.transform.position.y == info.transform.position.y)
            {
                if (noHurtboxesLastRound && info.enemyScript.characterID == "BoxerTeddy")//this is the script that kills teddy;
                {
                    
                    Crush();
                }
                else
                {
                    Push();
                }
            }
            else
            {
                if (info.transform.position.y > 0)
                {
                    Crush();
                }
            }
        }
        if(headpoint == new Vector3(0,0,-100))
        {
            noHurtboxesLastRound = true;
        }
        else
        {
            noHurtboxesLastRound = false;
        }
    }
}
