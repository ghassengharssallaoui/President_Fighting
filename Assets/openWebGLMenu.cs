using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openWebGLMenu : MonoBehaviour
{
    public ReceiveInputs receiver;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(receiver.myInput != null)
        {
            menu.active = true;
            gameObject.active = false;

        }
    }
}
