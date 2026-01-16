using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipFacingOnInput : MonoBehaviour
{
    public ReceiveInputs receiver;
    public PlayerInfo info;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        info = dummy.GetComponent<PlayerInfo>();
        receiver = dummy.GetComponent<PlayerInfo>().receiver;
    }

    // Update is called once per frame
    void Update()
    {
        if (receiver.moveVector.x > 0)
        {
            info.facing = 1;
            info.gameObject.transform.localScale = new Vector3(info.facing, 1, 1);
        }
        if (receiver.moveVector.x < 0)
        {
            info.facing = -1;
            info.gameObject.transform.localScale = new Vector3(info.facing, 1, 1);
        }
    }
}
