using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuCharacterSelect : MonoBehaviour
{
    public BasicMenuOption bmo;
    public BetterCameraMovement camScript;
    public bigEnabler bigEnable;
    public GameObject CharacterSelect;
    public GameObject p2display;
    public int previousA;
    public GameObject parentMenu;
    public float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
        camScript = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        bigEnable = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
        bmo = GetComponent<BasicMenuOption>();
        previousA = bmo.aPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterSelect.active == false) //stops the pause menu on character select from freaking out
        {
            if (bmo.aPress > previousA)
            {
                print("here");
                bigEnable.p1asset = null;
                bigEnable.p2asset = null;
                timer = 1;
                previousA = bmo.aPress;
                bigEnable.wipe.active = true;
                bigEnable.fakeEnable = true;

            }
            if (timer > 0)
            {
                timer += (1 / Time.timeScale) * Time.deltaTime * 60;
            }
            if (Mathf.Round(timer) == 10)
            {
                bool cpu;
                cpu = false;
                GameObject p1 = camScript.p1;
                GameObject p2 = camScript.p2;
                camScript.p1 = null;
                camScript.p2 = null;
                if (p2.GetComponent<PlayerInfo>().cpuLevel != 0)
                {
                    cpu = true;
                }
                Destroy(p1);
                Destroy(p2);
                print(p1.name);
                foreach (GameObject g in bigEnable.unorderedStuffToEnable)
                {
                    g.active = false;
                }
                Time.timeScale = 1;
                camScript.enabled = false;
                bigEnable.stage.active = false;
                camScript.gameObject.transform.position = new Vector3(0, 0, -20);
                camScript.ignoreZoom = false;
                camScript.ignoreBounds = false;
                CharacterSelect.active = true;
                parentMenu.active = false;
                if (cpu)
                {
                    p2display.active = false;
                }
            }
        }
        void OnDisable()
        {
            bigEnable.fakeEnable = false;


        }
    }
}

//