using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public bigEnabler be;
    public GameObject ready;
    public bool p1assigned;
    public bool p2assigned;
    public int rowCount;//gotta set this manually.
    public int arrayheight;
    public int arraylength;
    static int bitchvariable1;
    static int bitchvariable2;
    int testint;
    CharacterIcon[,] Icons;
    public CharacterIcon[] row1;
    public CharacterIcon[] row2;
    public CharacterIcon[] row3;
    public int P1x;
    public int P1y;//x&y position on the menut
    public int P2x;
    public int P2y;//x&y position on the menut
    public ReceiveInputs p1Input;
    public ReceiveInputs p2Input;
    public bool cpu;
    public bool cpuAssigned;
    void OnEnable()
    {
        bitchvariable1 = arrayheight;
        bitchvariable2 = arraylength;
        Icons = new CharacterIcon[bitchvariable1, bitchvariable2];
        p1Input = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        p2Input = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
        for(int i = 0; i < row1.Length; i++)
        {
            Icons[0, i] = row1[i];
        }
        for (int i = 0; i < row2.Length; i++)
        {
            Icons[1, i] = row2[i];
        }
        for (int i = 0; i < row3.Length; i++)
        {
            Icons[2, i] = row3[i];
        }
        p1assigned = false;
        p2assigned = false;
        cpu = false;
        ResetIcons();
    }
    void ResetIcons()
    {
        for (int i = 0; i < arrayheight; i++)
        {
            for(int j = 0; j < arraylength; j++)
            {
                if (Icons[i, j] != null)
                {
                    Icons[i, j].p1 = 0;
                    Icons[i, j].p2 = 0;
                }
            }
        }
    }
    void CheckAssigned() 
    {
        if (p1assigned == false)
        {
            if (GameObject.Find("P1Input") == null)
            {
                //do things to the display "waiting for input"

            }
            else
            {
                p1assigned = true;
                P1y = arrayheight - 1;
                P1x = 1;
                Icons[P1y, P1x].p1counter = 0;

            }
        }
        if (p2assigned == false)
        {
            if (GameObject.Find("P2Input") == null)
            {
                //do things to the display "waiting for input"
            }
            else
            {
                p2assigned = true;
                if (cpuAssigned == false)
                {
                    P2y = arrayheight - 1;
                    P2x = row1.Length - 2;
                    Icons[P2y, P2x].p2counter = 0;

                }
            }
        }
    }
    void ChangeHover(int x, int y, int player)
    {
        testint += 1;
        if (player == 1)
        {
            if (Icons[P1y, P1x].p1 <= 1) //lock the character until we downgrade
            {
                Icons[P1y, P1x].p1 = 0;
                P1x += x;
                P1y += y;
                Icons[P1y, P1x].p1counter = 0;

            }
            else if (Icons[P1y, P1x].p1 <= 1)
            {
                //do select interactions
            }
        }
        else
        {
            if (Icons[P2y, P2x].p2 <= 1) //lock the character until we downgrade
            {
                Icons[P2y, P2x].p2 = 0;
                Icons[P2y, P2x].p2counter = 0;
                P2x += x;
                P2y += y;
            }
        }
    }
    void NavigateMenu()
    {
        if (p1Input.holding[0] == 1)
        {
            if(P1y < arrayheight - 1)
            {
                ChangeHover(0, 1, 1);
            }
        }
        if (p1Input.holding[1] == 1)
        {
            if (P1y > 0)
            {
                ChangeHover(0, -1, 1);
            }
        }
        if (p1Input.holding[2] == 1)
        {
            if (P1x > 0 && Icons[P1y,P1x - 1] != null)
            {
                ChangeHover(-1, 0, 1);
            }
        }
        if (p1Input.holding[3] == 1)
        {
            if (P1x < row1.Length - 1 && Icons[P1y,P1x + 1] != null)
            {
                ChangeHover(1, 0, 1);
            }
        }
        if (p2Input.holding[0] == 1 || (p1Input.holding[0] == 1 && cpu))
        {
            if (P2y < arrayheight - 1)
            {
                ChangeHover(0, 1, 2);
            }
        }
        if (p2Input.holding[1] == 1 || (p1Input.holding[1] == 1 && cpu))
        {
            if (P2y > 0)
            {
                ChangeHover(0, -1, 2);
            }
        }
        if (p2Input.holding[2] == 1 || (p1Input.holding[2] == 1 && cpu))
        {
            if (P2x > 0 && Icons[P2y, P2x - 1] != null)
            {
                ChangeHover(-1, 0, 2);
            }
        }
        if (p2Input.holding[3] == 1 || (p1Input.holding[3] == 1 && cpu))
        {
            if (P2x < row1.Length - 1 && Icons[P2y, P2x + 1] != null)
            {
                ChangeHover(1, 0, 2);
            }
        }
        ///this is the navigation for both p1&p2
        if (p1assigned)
        {
            while (Icons[P1y,P1x] == null) 
            {
                if (P1x < row1.Length / 2)
                {
                    P1x += 1;
                }
                else
                {
                    P1x += -1;
                }
            }
            if (Icons[P1y, P1x].p1 == 0)
            {
                Icons[P1y, P1x].p1 = 1;
                Icons[P1y, P1x].p1counter = 0;

            }
        }
        if (p2assigned || cpu)
        {
            while (Icons[P2y,P2x] == null) 
            {
                if (P2x < row1.Length / 2)
                {
                    P2x += 1;
                }
                else
                {
                    P2x += -1;
                }
            }
            if (Icons[P2y, P2x].p2 == 0)
            {
                Icons[P2y, P2x].p2 = 1;
                Icons[P2y, P2x].p2counter = 0;

            }
        }
    }
    void Select() //Handles button pressing and the like.
    {
        if (p1Input.holdingAttack == 2 && cpu == false)
        {
            if (Icons[P1y, P1x].p1 < 3) //feel free to change if we need submenues. Same with the if stateument in Fixed Update.
            {
                Icons[P1y, P1x].p1 += 1;
                Icons[P1y, P1x].p1counter = 0;

            }
        }
        if (p1Input.holdingSpecial == 2 && cpu == false)
        {
            if (Icons[P1y, P1x].p1 > 0)
            {
                Icons[P1y, P1x].p1 += -1;
                Icons[P1y, P1x].p1counter = 0;

            }
        }
        if (p2Input.holdingAttack == 2 || (p1Input.holdingAttack == 2 && cpu))
        {
            if (Icons[P2y, P2x].p2 < 3)
            {
                Icons[P2y, P2x].p2 += 1;
                Icons[P2y, P2x].p2counter = 0;
                p1Input.holdingAttack = 3; //this prevents the "ready" from not appearing
                p2Input.holdingAttack = 3;
            }
        }
        if (p2Input.holdingSpecial == 2 || (p1Input.holdingSpecial == 2 && cpu))
        {
            if (Icons[P2y, P2x].p2 > 0)
            {
                Icons[P2y, P2x].p2 += -1;
                Icons[P2y, P2x].p2counter = 0;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        NavigateMenu();
        Select();
        CheckAssigned();
        if(p1assigned)
        {
            if (Icons[P1y, P1x].p1 == 3)
            {
                if (p2assigned)
                {
                    cpu = false;
                }
                else
                {
                    cpu = true;
                    if(cpuAssigned == false)
                    {
                        P2y = arrayheight - 1;
                        P2x = row1.Length - 2;
                        cpuAssigned = true;
                        Icons[P2y, P2x].p2counter = -1;

                    }
                }
            }
            else
            {
                cpu = false;
            }
        }
        if (p1assigned && (p2assigned || cpuAssigned))
        {
            if (Icons[P2y, P2x].p2 == 3 && Icons[P1y, P1x].p1 == 3)
            {
                if ((p1Input.holdingAttack == 2 || p2Input.holdingAttack == 2) && ready.active == true)
                {
                    ready.active = false;
                    be.doEnable = true;
                    if(cpu == false)
                    {
                        be.cpuLevel = 0;
                    }
                }
                ready.active = true;
            }
            else
            {
                ready.active = false;
            }
        }
    }


}
