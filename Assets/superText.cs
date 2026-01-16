using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superText : MonoBehaviour
{
    public PlayerInfo info;
    public GameObject animation;
    public GameObject loopAnimation;
    int timer;
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
        if (info.superCost <= info.superCharge && info.superCost != 0 && info.health >= 0)
        {
            timer += 1;
        }
        else
        {
            timer = 0;
            animation.active = false;
            loopAnimation.active = false;
        }
        if(timer == 30)
        {
            animation.active = true;
        }
    }
}
