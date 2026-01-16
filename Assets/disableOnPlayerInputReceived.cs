using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableOnPlayerInputReceived : MonoBehaviour
{
    MenuManager manager;
    public int player;
    public bool cpu;
    int value = 5;
    ReceiveInputs p1Input;
    public GameObject[] cpuLevelSprites;
    public bigEnabler be;
    void Start()
    {
        manager = GameObject.Find("Player Select Manager").GetComponent<MenuManager>();
        p1Input = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        be = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (cpu)
        {
            if(manager.cpu == false)
            {
                gameObject.active = false;
            }
            else
            {
                if (p1Input.holding[0] == 2 && value < 9)
                {
                    value += 1;
                }
                if (p1Input.holding[1] == 2 && value > 1)
                {
                    value += -1;
                }
            }
            foreach(GameObject g in cpuLevelSprites)
            {
                g.active = false;
            }
            cpuLevelSprites[value-1].active = true;
            be.cpuLevel = value;
        }
        else
        {
            if (player == 1)
            {
                if (manager.p1assigned)
                {
                    gameObject.active = false;
                }
            }
            if (player == 2)
            {
                if (manager.p2assigned || manager.cpu)
                {
                    gameObject.active = false;
                }
            }
        }
    }
}
