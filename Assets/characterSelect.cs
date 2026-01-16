using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterSelect : MonoBehaviour
{
    public p1SelectorThrobber selector;
    public GameObject menu;
    public int player; //cpu == 3 or 0. any != 1 or 2
    public ReceiveInputs inputs;
    public GameObject playerPrefab;
    public GameObject cpuMenu;
    public GameObject StageSelectMenu;
    public GameObject parentMenu;
    bigEnabler enabler;
    // Start is called before the first frame update
    void OnEnable()
    {
        enabler = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
        if (player == 1)
        {
            inputs = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
            selector.p1Selected = true;
            selector.p1 = true;
            enabler.p1asset = playerPrefab;
            if (GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>().myInput == null)
            {
                cpuMenu.active = true;
            }

        }
        else if(player == 2)
        {
            inputs = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
            selector.p2Selected = true;
            selector.p2 = true;
            enabler.p2asset = playerPrefab;


        }
        else //cpu
        {
            inputs = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
            selector.p2Selected = true;
            selector.p2 = true;

        }
        selector.selectCounter = 1;
        if(enabler.p1asset != null && enabler.p2asset != null)
        {
            StageSelectMenu.active = true;
            parentMenu.active = false;
            //enabler.doEnable = true;
        }
        //set other character related stuff
    }
    void Update()
    {
        if(inputs.holdingSpecial == 3)
        {
            gameObject.active = false;
            cpuMenu.active = false;
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        menu.active = true;
        if (player == 1)
        {
            selector.p1Selected = false;
            selector.p1 = false;
            if (enabler.p1asset == null || enabler.p2asset == null)
            {
                enabler.p1asset = null;
            }

        }
        else
        {
            selector.p2Selected = false;
            selector.p2 = false;
            if (enabler.p1asset == null || enabler.p2asset == null)
            {
                enabler.p2asset = null;
            }
        }
        selector.selectCounter = 0;
    }
}
