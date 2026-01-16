using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateOnGrounding : MonoBehaviour
{
    public GameObject[] activate;
    public GameObject[] deactivate;
    hitbox hbox;
    PlayerInfo info;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        info = dummy.GetComponent<PlayerInfo>();
    }
    void FixedUpdate()
    {
        if (info.traj.y + info.gameObject.transform.position.y <= 0)
        {
            foreach (GameObject g in activate)
            {
                g.active = true;
            }
            foreach (GameObject g in deactivate)
            {
                g.active = false;
            }
        }
    }
}
