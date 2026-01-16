using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionOnHit : MonoBehaviour
{
    PlayerInfo info;
    public int hitNum;
    public GameObject next;
    void OnEnable()
    {
        if (info == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<PlayerInfo>();
        }        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(info.hit == hitNum)
        {
            next.active = true;
            gameObject.active = false;
        }
    }
}
