using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMenu : MonoBehaviour
{
    public GameObject[] menuOptions;
    public int player;
    public ReceiveInputs inputs;
    public int currentSelection;
    public bool dontResetHoverOnDisable;
    public bool dontResetSelection;
    public bool backOnB;
    public GameObject previousMenu;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (player == 1)
        {
            inputs = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        }
        else
        {
            inputs = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
        }
        HardResetHovering();

        if (dontResetSelection == false)
        {
            currentSelection = 0;
        }
    }
    void HardResetHovering()
    {
        for(int i = 0; i < menuOptions.Length; i++)
        {
            if(i != currentSelection)
            {
                if (menuOptions[i].GetComponent<BasicMenuOption>() != null)
                {
                    menuOptions[i].GetComponent<BasicMenuOption>().hovering = false;
                }
                else
                {
                    //add controller modify stuff
                    //add controller modify stuff
                }
            }
        }
    }
    void BasicMenuOptionUpdate()
    {
        BasicMenuOption bmo = menuOptions[currentSelection].GetComponent<BasicMenuOption>();
        bmo.hovering = true;
        bmo.myMenu = GetComponent<VerticalMenu>();
        if (inputs.TRUEholdingAttack == 2)
        {
            bmo.aPress += 1;
        }
        if (inputs.TRUEholdingSpecial == 2)
        {
            bmo.bPress += 1;
        }
        if (inputs.TRUEholding[2] == 2)
        {
            bmo.lPress += 1;
        }
        if (inputs.TRUEholding[3] == 2)
        {
            bmo.rPress += 1;
        }
    }
    void MoveSelector()
    {
        if (inputs.TRUEholding[0] == 2 && currentSelection > 0)
        {
            currentSelection += -1;
            HardResetHovering();
        }
        if (inputs.TRUEholding[1] == 2 && currentSelection < menuOptions.Length - 1)
        {
            currentSelection += 1;
            HardResetHovering();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(backOnB && inputs.TRUEholdingSpecial == 2)
        {
            previousMenu.active = true;
            gameObject.active = false;
        }
        //print(inputs.holding[0]);
        if (menuOptions[currentSelection].GetComponent<BasicMenuOption>() != null)
        {
            BasicMenuOptionUpdate();
        }
        else
        {
            //this is for the controller change section
        }
        MoveSelector();
    }
    void OnDisable()
    {
        if(dontResetHoverOnDisable == false)
        {
            currentSelection = 0;
            HardResetHovering();
        }
    }
}
