using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuRematch : MonoBehaviour
{
    BasicMenuOption bmo;
    BetterCameraMovement camScript;
    bigEnabler bigEnable;
    int previousA;
    public GameObject parentMenu;
    int timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
        camScript = GameObject.Find("PostGameMenu").GetComponent<activateMenu>().camScript;
        bigEnable = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
        bmo = GetComponent<BasicMenuOption>();
        previousA = bmo.aPress;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bmo.aPress > previousA)
        {
            bigEnable.doEnable = true;
            timer = 1;
            previousA = bmo.aPress;
            bigEnable.wipe.active = true;
        }
        if(timer > 0)
        {
            timer += 1;
        }
        if (timer == 10)
        {
            Time.timeScale = 1;
            parentMenu.active = false;
            GameObject p1 = camScript.p1;
            GameObject p2 = camScript.p2;
            camScript.p1 = null;
            camScript.p2 = null;
            Destroy(p1);
            Destroy(p2);
            foreach (GameObject g in bigEnable.unorderedStuffToEnable)
            {
                g.active = false;
            }
            timer = 0;
        }
    }
}
