using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayWinningCharText : MonoBehaviour
{
    PlayerInfo p1;
    PlayerInfo p2;
    BetterCameraMovement cam;
    // Start is called before the first frame update
    void OnEnable()
    {
        cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        p1 = cam.GetComponent<BetterCameraMovement>().p1.GetComponent<PlayerInfo>();
        p2 = cam.GetComponent<BetterCameraMovement>().p2.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(p1.hit == 100)
        {
            GetComponent<Text>().text = "1";
        }
        else
        {
            GetComponent<Text>().text = "2";
        }
    }
}
