using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superMeterStarSpin : MonoBehaviour
{
    public PlayerInfo info;
    float spinCounter;
    public float spinTime;
    public float spinDistance;
    // Start is called before the first frame update
    void OnEnable()
    {

            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<healthBarGod>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<healthBarGod>().info;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float one = 1;
        if (info.player == 2)
        {
            one = -1;
        }
        float a = info.superCharge;
        float b = info.superCost;
        if (a >= b)
        {
            a = b;
            spinCounter += 0.5f * (.1f + 0.9f * (a / b) * (a / b) * (a / b));
        }
        spinCounter += .1f + 0.9f * (a / b)*(a/b)*(a/b);
        if (spinCounter > spinTime)
        {
            transform.eulerAngles += new Vector3(0, 0, one * spinDistance);
            spinCounter = 0;
        }
        
    }
}
