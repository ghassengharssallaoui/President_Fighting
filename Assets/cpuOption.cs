using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpuOption : MonoBehaviour
{
    public bool move;
    public int strength;
    public bool reverseMoveX;
    public string expectedAnim;
    public int totalTime;
    //[HideInInspector] 
    public int timer;
    public cpuCommand[] commands;
    public string[] requiredAnims;
    public string[] forbiddenAnims;
    public PlayerInfo info;
    public ReceiveInputs receiver;
    public int resetFrame; //the last frame in the whole thing. Willoften be 1 for simple inputs
    public bool collision;
    public bool parentIsOwner = true;
    hitbox hb;
    void Start()
    {
        hb = GetComponent<hitbox>();
        if (parentIsOwner)
        {
            int i = 0;
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<commandsList>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            while (dummy.GetComponent<commandsList>().commands[i] != null)
            {
                i++;
            }
            dummy.GetComponent<commandsList>().commands[i] = gameObject.GetComponent<cpuOption>();
        }
        else //This is for enemyOptions that try and make the main guy jump over projectiles, etc
        {
            //this will need to be updated as we keep adding more ways to assign things to players.
            GameObject dummy;
            dummy = gameObject;
            PlayerInfo dummyInfo;
            while (dummy.GetComponent<PlayerInfo>() == null && dummy.GetComponent<hitBoxAssigner>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            if(dummy.GetComponent<PlayerInfo>() != null)
            {
                dummyInfo = dummy.GetComponent<PlayerInfo>();
            }
            else if(dummy.GetComponent<hitBoxAssigner>() != null)
            {
                dummyInfo = dummy.GetComponent<hitBoxAssigner>().info;
            }
            else
            {
                dummyInfo = GameObject.Find("Washington").GetComponent<PlayerInfo>();
            }
            int i = 0;
            while (dummyInfo.enemyScript.airCPUCommands.commands[i] != null)
            {
                i++;

            }
            dummyInfo.enemyScript.airCPUCommands.commands[i] = gameObject.GetComponent<cpuOption>();
            i = 0;
            while (dummyInfo.enemyScript.groundCPUCommands.commands[i] != null)
            {
                i++;
            }
            dummyInfo.enemyScript.groundCPUCommands.commands[i] = gameObject.GetComponent<cpuOption>();
            if (dummyInfo.player == 1)
            {
                hb.p2collide = true;
                hb.p2default = true;
                hb.p1collide = false;
                hb.p1default = false;
            }
            else
            {
                hb.p2collide = false;
                hb.p2default = false;
                hb.p1collide = true;
                hb.p1default = true;
            }
        }
        timer = -1;
        foreach(cpuCommand c in commands)
        {
            if (c.end == 0)
            {
                c.end = c.start + 3;
            }
            if (resetFrame < c.end)
            {
                resetFrame = c.end;
            }
        }
    }
    bool confirmHit()
    {
        foreach (hurtbox h in hb.hurtboxesHit)
        {
            if (h != null)
            {
                if(h.type == "" || h.type == "Counter")
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool confirmAnim(bool b)
    {
        if(b == false)
        {
            return false;
        }
        else
        {
            if (forbiddenAnims.Length > 0) 
            {
                foreach (string s in forbiddenAnims)
                {
                    if (s == info.currentAnim.name)
                    {
                        return false;
                    }
                } 
            }
            if(requiredAnims.Length > 0)
            {
                foreach (string s in requiredAnims)
                {
                    if (s == info.currentAnim.name)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        collision = confirmHit();
        collision = confirmAnim(collision);
        if(timer == 2)
        {
            if(info.currentAnim.name != expectedAnim && expectedAnim != "")
            {
                timer = resetFrame;
                info.cpuCounter = 0;
            }
        }
        if (info != null)
        {
            totalTime = resetFrame + 1;
            if (timer == 0)
            {

                int defaultWait = 10 + 60 / info.cpuLevel;
                if (defaultWait > totalTime && move)
                {
                    info.cpuCounter = 10 + 60 / info.cpuLevel;//change this when it comes time to tweak levels

                }
                else
                {
                    info.cpuCounter = totalTime;

                }
            }
            if (timer > totalTime || timer < 0)
            {
                timer = -1;
            }
            else
            {
                timer += 1;
            }
            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i].start <= timer && commands[i].end > timer)
                {
                    receiver.attack = commands[i].Attack;
                    receiver.jump = commands[i].Jump;
                    receiver.super = commands[i].Super;
                    receiver.special = commands[i].Special;
                    receiver.movement = commands[i].Movement;
                    if (commands[i].moveVector != new Vector2(0, 0) || commands[i].stop)
                    {
                        if (reverseMoveX)
                        {
                            receiver.moveVector = new Vector2(commands[i].moveVector.x * -1 * info.facing, commands[i].moveVector.y);
                        }
                        else
                        {
                            receiver.moveVector = new Vector2(commands[i].moveVector.x * info.facing, commands[i].moveVector.y);
                        }
                    }
                }
                if (timer > resetFrame)
                {
                    receiver.attack = false;
                    receiver.special = false;
                    receiver.super = false;
                    receiver.movement = false;
                    receiver.jump = false;
                    if (move == false)
                    {
                        receiver.moveVector = new Vector2(0, 0);
                    }
                }
            }
        }
    }
}
