using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    public GameObject dash;
    public GameObject punch;
    ReceiveInputs input;
    PlayerInfo info;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (input == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<PlayerInfo>();
            input = info.receiver;
        }
        if ((input.holding[2] > 0 && info.facing == -1) || (input.holding[3] > 0 && info.facing == 1))
        {
            dash.active = true;
        }
        else
        {
            punch.active = true;
        }
        gameObject.active = false;
    }


}
