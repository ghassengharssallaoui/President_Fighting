using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuActivator : MonoBehaviour
{
    public ReceiveInputs p1Input;
    ReceiveInputs p2Input;
    public GameObject[] p1Menu;
    public GameObject[] waitingForP1Text;
    public GameObject[] waitingForP2Text;
    public GameObject[] p2Menu;
    
    public GameObject[] cpuMenu;//I'll come back to this.
    public bool p1Activated;
    public bool p2Activated;
    bigEnabler big;
    VersionDecider version;
    // Start is called before the first frame update

        void Start()
    {
        p1Input = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        p2Input = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
        big = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
        version = GameObject.Find("VersionDecider").GetComponent<VersionDecider>();

    }
    void OnEnable()//This code fixed a bug with webgl that i didn't understand, BUT it did fix things through trial and error
    {
        p1Activated = false;
        p2Activated = false;
        if (version != null)
        {
            if (version.WebGL)
            {
                p1Activated = false;
                p2Activated = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

        if(Time.timeScale == 1 && p1Input.myInput != null )
        {
            if (p1Activated == false)
            {
                foreach (GameObject g in p1Menu)
                {
                    g.active = true;
                }
                foreach (GameObject g in waitingForP1Text)
                {
                    g.active = false;
                }
                p1Activated = true;
            }
        }
        else
        {
            foreach (GameObject g in p1Menu)
            {
                g.active = false;
            }
            foreach (GameObject g in waitingForP1Text)
            {
                g.active = true;
            }
            p1Activated = false;
        }
        ///
        if ((Time.timeScale == 1 && p2Input.myInput != null) || (p1Activated && version.WebGL))
        {
            big.cpuLevel = 0;
            if (p2Activated == false)
            {
                foreach (GameObject g in p2Menu)
                {
                    g.active = true;
                }
                foreach (GameObject g in cpuMenu)
                {
                    g.active = false;
                }
                foreach (GameObject g in waitingForP2Text)
                {
                    g.active = false;
                    
                }
                p2Activated = true;
            }
        }
        else
        {
            foreach (GameObject g in p2Menu)
            {
                g.active = false;
            }
            foreach (GameObject g in waitingForP2Text)
            {
                g.active = true;
            }
            p2Activated = false;
        }
    }
}
