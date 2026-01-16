using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class true2Player : MonoBehaviour
{
    public GameObject menu;
    public ReceiveInputs receiver;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("VersionDecider").GetComponent<VersionDecider>().WebGL == true)
        {
            done = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(done == false)
        {
            if(receiver.myInput != null)
            {
                if (receiver.myInput.isKeyboard)
                {
                    menu.active = true;
                    done = true;
                }
            }
        }
    }
}
