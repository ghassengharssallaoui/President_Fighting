using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigEnabler : MonoBehaviour
{
    public GameObject p1asset;
    public GameObject p2asset;
    public GameObject stage;
    public GameObject[] unorderedStuffToEnable;
    public GameObject[] unorderedStuffToDisable;
    public int cpuLevel;
    public bool doEnable;
    public bool fakeEnable;
    public BetterCameraMovement camScript;
    public GameObject wipe;
    public int timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (doEnable)
        {
            fakeEnable = false;
            if (timer > 10)
            {
                camScript.enabled = true;
                doEnable = false;
                Time.timeScale = 0.00001f;
                stage.active = true;
                foreach (GameObject g in unorderedStuffToDisable)
                {
                    g.active = false;
                }
                foreach (GameObject g in unorderedStuffToEnable)
                {
                    g.active = true;
                }
                GameObject dummy = Instantiate(p1asset, new Vector3(-5, 0, 0), Quaternion.identity);
                PlayerInfo dummyInfo;
                if (dummy.GetComponent<PlayerInfo>() == null)
                {
                    dummy = dummy.transform.GetChild(0).gameObject;

                }
                dummyInfo = dummy.GetComponent<PlayerInfo>();
                dummy.transform.localScale = new Vector3(1, 0, 0);
                dummyInfo.facing = 1;
                dummyInfo.player = 1;
                GameObject dummy2 = Instantiate(p2asset, new Vector3(5, 0, 0), Quaternion.identity);
                PlayerInfo dummyInfo2;
                if (dummy2.GetComponent<PlayerInfo>() == null)
                {
                    dummy2 = dummy2.transform.GetChild(0).gameObject;

                }
                dummyInfo2 = dummy2.GetComponent<PlayerInfo>();
                dummy2.transform.localScale = new Vector3(-1, 0, 0);
                dummyInfo2.facing = -1;
                dummyInfo2.player = 2;
                dummyInfo.enemyObject = dummy2;
                dummyInfo.enemyScript = dummyInfo2;
                dummyInfo2.enemyObject = dummy;
                dummyInfo2.enemyScript = dummyInfo;
                //health
                dummyInfo.health = dummyInfo.maxHealth;
                dummyInfo2.health = dummyInfo2.maxHealth;
                //cpu
                dummyInfo2.cpuLevel = cpuLevel;
                camScript.p1 = dummy;
                camScript.p2 = dummy2;
                camScript.ignoreZoom = false;
                camScript.ignoreBounds = false;

                timer = 0;
                if (GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>().holdingAttack >= 2)
                {
                    GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>().holdingAttack = 0;
                }
                if (GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>().holdingAttack >= 2)
                {
                    GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>().holdingAttack = 0;
                }
            }
            else if(timer == 0)
            {
                wipe.active = true;
                timer += 1;

            }
            else 
            {
                timer += 1;
            }
        }
    }
}
