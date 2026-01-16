using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superMeter : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerInfo info;
    public int superID;
    public GameObject animation;
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
    void Update()
    {
        if (info.superCharge >= superID && info.superCost >= superID)
        {
            animation.active = true;
        }
        else
        {
            animation.active = false;
        }
        if(info.superCost < superID)
        {
            GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);

        }
    }
}
