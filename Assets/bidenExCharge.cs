using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bidenExCharge : MonoBehaviour
{
    public PlayerInfo info;
    public hitbox hbox;

    void Start()
    {
        hbox = GetComponent<hitbox>();
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null && dummy.GetComponent<hitBoxAssigner>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        if (dummy.GetComponent<PlayerInfo>() != null)
        {
            info = dummy.GetComponent<PlayerInfo>();
        }
        if (dummy.GetComponent<hitBoxAssigner>() != null)
        {
            info = dummy.GetComponent<hitBoxAssigner>().info;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (hurtbox h in hbox.hurtboxesHit)
        {
            if (h != null)
            {
                if (h.player == 3)//hit a third party.
                {

                }
                else if (h.player != info.player) //hit the opponent
                {
                    info.GetComponent<bidenBlastDeciderEnabler>().giveCharge = true;
                }
            }
        }
    }
    void OnDisable()
    {
        FixedUpdate();
    }
}
