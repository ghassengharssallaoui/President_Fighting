using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidenExDecider : MonoBehaviour
{
    ReceiveInputs receiver;
    public ObamaChargeUI uiScript;
    public GameObject ex;
    public GameObject normal;

    // Start is called before the first frame update
    void Update()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        receiver = dummy.GetComponent<PlayerInfo>().receiver;

        if (receiver.holdingMovement > 0 && uiScript.charge > uiScript.totalChargeTime)
        {
            ex.active = true;
            uiScript.charge = 0;
        }
        else
        {
            normal.active = true;
        }
        gameObject.active = false;
    }

}
