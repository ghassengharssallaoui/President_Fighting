using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPoint : MonoBehaviour
{
    public PlayerInfo info;
    public int healthID;
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
        if(info.health + 1 < healthID)
        {
            animation.active = true;
        }
        else
        {
            animation.active = false;
        }
    }
}
