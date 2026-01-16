using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicHit : MonoBehaviour
{
    PlayerInfo info;
    GameObject player;
    public int hitTimer;
    public int knockbackPoint;
    public Vector3 knockbackAngle;
    public GameObject[] hurtboxes;
    Vector3 initialVect;
    public GameObject flyBack;
    bool mightHaveToCombo;
    int lastHit;
    void OnEnable()
    {
        if (info == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            player = dummy;
            info = player.GetComponent<PlayerInfo>();
        }
        lastHit = info.hit;
        hitTimer = 0;
        initialVect = transform.position;
        info.traj = new Vector3(0.05f * info.facing * -1, 0, 0);
        float xdir;
        xdir = (-1 * (info.gameObject.transform.position.x - info.enemyObject.transform.position.x) / Mathf.Abs(info.gameObject.transform.position.x - info.enemyObject.transform.position.x));
        int intx = 1;
        if (xdir > 0)
        {
            intx = 1;
        }
        else
        {
            intx = -1;
        }
        info.facing = intx;
        if(info.hit == -3)
        {
            foreach(GameObject g in hurtboxes)
            {
                g.active = true;
            }
        }
        mightHaveToCombo = false;
    }
    void CheckCombo()
    {
        if(info.hit != -1)
        {
            mightHaveToCombo = true;
        }
        if(mightHaveToCombo && info.hit == -1)
        {
            info.health += -1;
            info.enemyScript.superCharge += 1;
            mightHaveToCombo = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(info.hit != lastHit)//this makes it play again if we do combos
        {
            GetComponent<Options>().enabled = false;
            GetComponent<Options>().enabled = true;
            lastHit = info.hit;
        }
        CheckCombo();
        hitTimer += 1;
        if (info.hit == -1)
        {
            if (hitTimer <= 30)
            {
                if (hitTimer % 3 == 0)
                {
                    info.traj = new Vector3(info.traj.x * -1, 0, 0);
                }
            }
            else
            {
                info.resolveHit = true;
                flyBack.active = true;
                gameObject.active = false;
            }
        }
        if(info.hit == -3)
        {
            if (hitTimer <= knockbackPoint)
            {
                if (hitTimer % 3 == 0)
                {
                    info.traj = new Vector3(info.traj.x * -1, 0, 0);
                    
                }
            }
            else
            {
                if (knockbackPoint != 0)
                {
                    //do combo fly 
                }
                else
                {
                    info.transform.position += new Vector3(0, 0.001f, 0);
                    info.GetComponent<OptionsReference>().airDefault.active = true;
                    info.hit = 0;
                    gameObject.active = false;
                    info.traj = knockbackAngle;
                }
            }
        }
    }
    void OnDisable()
    {
        foreach (GameObject g in hurtboxes)
        {
            g.active = false;
        }
    }
}

