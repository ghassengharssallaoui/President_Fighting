using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class versionDependentEnabler : MonoBehaviour
{
    bool WebGL;
    bool mobile;
    public GameObject[] webObjects;
    public GameObject[] mobileObjects;
    public GameObject[] desktopObjects;
    public bool dontDisable;
    bool doit;
    // Start is called before the first frame update
    void OnEnable()
    {
        doit = true;
    }
    void Update()//had to delay a frame to deal with the false prophet
    {
        if (doit)
        {
            doit = false;
            WebGL = GameObject.Find("VersionDecider").GetComponent<VersionDecider>().WebGL;
            mobile = GameObject.Find("VersionDecider").GetComponent<VersionDecider>().mobile;
            if (WebGL)
            {
                foreach (GameObject g in webObjects)
                {
                    g.active = true;
                }
                if (dontDisable == false)
                {
                    gameObject.active = false;
                }
            }
            else if (mobile)
            {
                foreach (GameObject g in mobileObjects)
                {
                    g.active = true;
                }
                if (dontDisable == false)
                {
                    gameObject.active = false;
                }
            }
            else
            {
                foreach (GameObject g in desktopObjects)
                {
                    g.active = true;
                }
                if (dontDisable == false)
                {
                    gameObject.active = false;
                }
            }

        }
    }
}
