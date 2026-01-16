using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSuperText : MonoBehaviour
{
    public PlayerInfo info;
    public GameObject superText;
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
            if (info.superCharge >= info.superCost)
            {
                superText.active = true;
            }
            else
            {
                superText.active = false;
            }
        }
    }
}
