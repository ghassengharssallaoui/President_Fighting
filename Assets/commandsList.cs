using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commandsList : MonoBehaviour
{
    public cpuOption[] commands = new cpuOption[100];
    public PlayerInfo playerSetByHand;
    
    void Update()
    {
        if (playerSetByHand.hit != 0)
        {
            foreach (cpuOption c in commands)
            {
                if (c != null)
                {
                    c.timer = -1;
                    playerSetByHand.cpuCounter = 0;
                }
            }
        }
        foreach(cpuOption c in commands)
        {
            if (c != null)
            {
                c.info = playerSetByHand;
                if (playerSetByHand.player == 1)
                {
                    c.receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
                    hitbox h = c.GetComponent<hitbox>();
                    if(h.p1collide == false && h.p2collide == false)
                    {
                        h.p2collide = true;
                        h.p2default = true;
                    }
                }
                else
                {
                    c.receiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
                    hitbox h = c.GetComponent<hitbox>();
                    if (h.p1collide == false && h.p2collide == false)
                    {
                        h.p1collide = true;
                        h.p1default = true;
                    }
                }
            }
        }
    }
}
