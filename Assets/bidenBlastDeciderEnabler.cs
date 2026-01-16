using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bidenBlastDeciderEnabler : MonoBehaviour
{
    public int blastPower;
    public GameObject[] objs;
    public bidenBlastDeciderEnabler parentDecider;
    public bool boss;
    public bool giveCharge;
    public bool waitingOnHit1;
    public bool dontDisable;
    // Start is called before the first frame update
    void OnEnable()
    {
        waitingOnHit1 = true;
        if (boss == false && dontDisable == false)
        {
            if (parentDecider != null)
            {
                blastPower = parentDecider.blastPower;
            }
            objs[blastPower].active = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(blastPower > 7)
        {
            blastPower = 7;
        }
        if (dontDisable == false)
        {
            if (boss)
            {
                if (GetComponent<PlayerInfo>().enemyScript.characterID == "JFK")
                {
                    if (blastPower <= 3)
                    {
                        blastPower = 3;
                    }
                }
                if (waitingOnHit1 == true)
                {
                    if (GetComponent<PlayerInfo>().hit == 1)
                    {
                        waitingOnHit1 = false;
                    }
                }
                if (giveCharge)
                {
                    if (GetComponent<PlayerInfo>().hit == 0 && waitingOnHit1 == false)
                    {
                        waitingOnHit1 = true;
                        blastPower += 1;
                        giveCharge = false;
                    }
                }
                if (GetComponent<PlayerInfo>().hit == 0)
                {
                    waitingOnHit1 = true;
                }
            }
            if (boss == false)
            {
                objs[blastPower].active = false;
                gameObject.active = false;
            }
        }
        else
        {
            blastPower = parentDecider.blastPower;
            foreach (GameObject g in objs)
            {
                if(g != objs[blastPower])
                {
                    g.active = false;
                }
            }
            objs[blastPower].active = true;
        }
    }
}
