using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidenBlastHitboxInterpreter : MonoBehaviour
{
    public Transform enemyPosition;
    public PlayerInfo info;
    public bool doit;
    public hitbox hbox;
    public int  startingFacing;
    projectileTraj myTraj;
    public enemyDragDown dragger;
    // Start is called before the first frame update
    void Start()
    {
        hbox = GetComponent<hitbox>();
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null && dummy.GetComponent<hitBoxAssigner>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        if (dummy.GetComponent<PlayerInfo>() != null)
        {
            info = dummy.GetComponent<PlayerInfo>();
        }
        if (dummy.GetComponent<hitBoxAssigner>() != null)
        {
            info = dummy.GetComponent<hitBoxAssigner>().info;
        }
        myTraj = dummy.GetComponent<projectileTraj>();
        startingFacing = info.facing;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (hurtbox h in hbox.hurtboxesHit)
        {
            if (h != null)
            {



                    if (h.player == 3)//hit a third party.
                    {

                    }
                    else if (h.player != info.player) //hit the opponent
                    {
                    dragger.enemy = info.enemyScript;
                    dragger.facing = startingFacing;
                    }
                
            }
        }
    }
    void Update()
    {

    }
}
