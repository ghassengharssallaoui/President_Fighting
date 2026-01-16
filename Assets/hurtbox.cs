using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtbox : MonoBehaviour
{
    hurtboxManager hm;
    public int player;
    public bool thirdparty; //may NEVER use this, but this is to indicate a hurtbox that should be hit by p1 or p2 enabled hitboxes.
    public bool projectileImmune;
    public int hmID;//id in the hm array.
    int counter;
    public string type;
    public int typeID; //ProjectileStrengthEtc.
    public GameObject destroyObject;
    // Start is called before the first frame update
    void OnEnable()
    {
        counter = 0;
        if(hm == null)
        {
            hm = GameObject.Find("HurtboxManager").GetComponent<hurtboxManager>();
        }
        if (player == 0)
        {
            if (thirdparty)
            {
                player = 3;
            }
            else 
            {
                int failsafe;
                failsafe = 0;
                GameObject dummy;
                dummy = gameObject;
                while (dummy.GetComponent<PlayerInfo>() == null && failsafe < 10)
                {
                    dummy = dummy.transform.parent.gameObject;
                    failsafe += 1;
                }
                player = dummy.GetComponent<PlayerInfo>().player;
            }
        }
        if(player == 1 || thirdparty)
        {
            while (hm.p1hurtboxes[counter] != null)
            {
                counter++;
            }
            hm.p1hurtboxes[counter] = gameObject;
            //do the MASSIVE array updating;
        }
        if(player == 2 || thirdparty) 
        {
            while (hm.p2hurtboxes[counter] != null)
            {
                counter++;
            }
            hm.p2hurtboxes[counter] = gameObject;
        }
    }
    void OnDisable()
    {
        if (player == 1 || thirdparty)
        {
            hm.p1hurtboxes[counter] = null;
        }
        if (player == 2 || thirdparty)
        { 
            hm.p2hurtboxes[counter] = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
