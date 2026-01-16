using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateMenu : MonoBehaviour
{
    public BetterCameraMovement camScript;
    public GameObject menu;
    public bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(camScript.p1 != null && camScript.p2 != null)
        {
            PlayerInfo info = camScript.p1.GetComponent<PlayerInfo>();
            PlayerInfo info2 = camScript.p2.GetComponent<PlayerInfo>();
            if(info.hit == 100 || info2.hit == 100)
            {
                if (done == false)
                {
                    menu.active = true;
                    camScript.enabled = false;
                    done = true;
                }
            }
            else
            {
                done = false;
                menu.active = false;
            }
        }
        else
        {
            
            menu.active = false;
        }
    }
}
