using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonFatalGetUp : MonoBehaviour
{
    Options opt;
    public int waitFrames;
    int timer;
    PlayerInfo enemyInfo;
    bool done;
    // Start is called before the first frame update
    void OnEnable()
    {
        opt = GetComponent<Options>();
        timer = 0;
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        enemyInfo = dummy.GetComponent<PlayerInfo>().enemyScript;
    }
    void FixedUpdate()
    {
        if (timer != 0)
        {
            if (timer == waitFrames)
            {
                opt.currentAnim = 1;
                opt.currentAnimFrame = 1;
            }
            timer += 1;
        }
        else
        {
            if(enemyInfo.hit == 100 && done == false)
            {
                done = true;
                opt.currentAnim = 1;
                opt.currentAnimFrame = 1;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale < 0.1f)
        {
            FixedUpdate();
        }
    }
}
