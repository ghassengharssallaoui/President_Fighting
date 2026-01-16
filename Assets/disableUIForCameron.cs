using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableUIForCameron : MonoBehaviour
{
    VersionDecider version;
    ReceiveInputs inputs;
    public bool sprite;
    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        version = GameObject.Find("VersionDecider").GetComponent<VersionDecider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (version.camBuild == true)
        {
            if (inputs.holdingStart == 3)
            {
                if (sprite)
                {
                    GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    Destroy(gameObject);
                    //gameObject.active = false;
                }
            }
        }
    }
}
