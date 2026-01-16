using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarGod : MonoBehaviour
{
    
    public int player;
    public PlayerInfo info;
    public int needToEnable;
    bool alreadyEnabled;
    BetterCameraMovement cam;
    // Start is called before the first frame update
    void OnEnable()
    {
        cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        alreadyEnabled = false;
        needToEnable = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == 1)
        {
            if (cam.GetComponent<BetterCameraMovement>().p1 != null)
            {
                info = cam.GetComponent<BetterCameraMovement>().p1.GetComponent<PlayerInfo>();
                GetComponent<disableOnDisable>().disable.active = true;
            }
            else
            {
                GetComponent<disableOnDisable>().disable.active = false;
            }
        }
        else
        {
            if (cam.GetComponent<BetterCameraMovement>().p2 != null)
            {
                info = cam.GetComponent<BetterCameraMovement>().p2.GetComponent<PlayerInfo>();
                GetComponent<disableOnDisable>().disable.active = true;
            }
            else
            {
                GetComponent<disableOnDisable>().disable.active = false;
            }
        }
    }

}
