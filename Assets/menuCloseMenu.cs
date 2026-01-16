using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCloseMenu : MonoBehaviour
{
    BasicMenuOption bmo;
    public GameObject menuToOpen;
    public GameObject parentMenu;
    int previousA;
    // Start is called before the first frame update
    void OnEnable()
    {
        bmo = GetComponent<BasicMenuOption>();
        previousA = bmo.aPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (parentMenu == null)
        {
            if (bmo.aPress > previousA)
            {
                if (menuToOpen != null)
                {
                    menuToOpen.active = true;
                }
                bmo.myMenu.gameObject.active = false;
            }
        }
        else
        {
            if (bmo.aPress > previousA)
            {
                if (menuToOpen != null)
                {
                    menuToOpen.active = true;
                }
                parentMenu.active = false;
            }
        }
    }
}
