using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTriggerPause : MonoBehaviour
{
    ReceiveInputs p1inputs;
    ReceiveInputs p2inputs;
    bool allowpausep1; //these prevent pausing upon entering the game
    bool allowpausep2;
    VerticalMenu menu;
    public GameObject menuObject;
    public GameObject onlineMenu;
    public GameObject[] otherMenus; //this is actually ALL menus, forgive me future Xander but I'm not changing it
    public bool anyActive;
    public GameObject ButtonMappingMenu;
    public VersionDecider versionDecider;
    // Start is called before the first frame update
    void Start()
    {
        versionDecider = GameObject.Find("VersionDecider").GetComponent<VersionDecider>();
        p1inputs = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        p2inputs = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
        menu = menuObject.GetComponent<VerticalMenu>();
        if (versionDecider.client || versionDecider.server)
        {
            menuObject = onlineMenu;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool mappingMenuCheck;
        mappingMenuCheck = true;
        if(ButtonMappingMenu != null)
        {
            if(ButtonMappingMenu.active == true)
            {
                mappingMenuCheck = false;
            }
        }
        if (mappingMenuCheck)
        {
            anyActive = false;
            foreach (GameObject g in otherMenus)
            {
                if (g.active == true)
                {
                    anyActive = true;
                }
            }
            if (p1inputs.TRUEholdingStart == 2)
            {
                if (allowpausep1 == false)
                {
                    allowpausep1 = true;
                }
                else
                {
                    if (anyActive == false)
                    {
                        menu.player = 1;
                        menuObject.active = true;
                        foreach (GameObject g in otherMenus)
                        {
                            g.GetComponent<VerticalMenu>().player = 1;
                        }
                    }
                    else
                    {
                        foreach (GameObject g in otherMenus)
                        {
                            g.active = false;
                        }

                    }
                }
            }
            if (p2inputs.TRUEholdingStart == 2)
            {
                if (allowpausep2 == false)
                {
                    allowpausep2 = true;
                }
                else
                {
                    if (anyActive == false)
                    {
                        menu.player = 2;
                        menuObject.active = true;
                        foreach (GameObject g in otherMenus)
                        {
                            g.GetComponent<VerticalMenu>().player = 2;
                        }
                    }
                    else
                    {
                        foreach (GameObject g in otherMenus)
                        {
                            g.active = false;
                        }
                    }
                }
            }
        }
    }
}
