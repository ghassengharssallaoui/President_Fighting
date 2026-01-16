using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCancelOptions : MonoBehaviour
{
    public GameObject leftRight;
    PlayerInfo infoScript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (infoScript.receiver.holding[2] == 2)
        {
            infoScript.facing = -1;
            leftRight.active = true;
            gameObject.active = false;
        }
        if (infoScript.receiver.holding[3] == 2)
        {
            infoScript.facing = 1;
            leftRight.active = true;
            gameObject.active = false;
        }
    }
}
