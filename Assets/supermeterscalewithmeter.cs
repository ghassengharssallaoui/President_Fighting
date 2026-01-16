using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class supermeterscalewithmeter : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInfo info;
    public GameObject healthFire;
    // Start is called before the first frame update
    void OnEnable()
    {
        info = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (info == null)
        {
            if (transform.parent.GetComponent<superMeterGod>().info != null)
            {
                info = transform.parent.GetComponent<superMeterGod>().info;
            }
        }
        else
        {
            float one = 1;
            if(info.player == 2)
            {
                one = -1;
            }
            float a = info.superCharge;
            float b = info.superCost;
            if (a >= b)
            {
                a = b;
                healthFire.active = false;
            }
            else
            {
                healthFire.active = true;
            }
            transform.localScale = new Vector3(one* a/b, 1, 1);
        }
    }
}
