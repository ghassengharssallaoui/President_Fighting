using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exbidenblast : MonoBehaviour
{
    PlayerInfo info;
    BetterCameraMovement cam;
    Vector3 traj;
    public int counter;
    // Start is called before the first frame update
    void OnEnable()
    {
        cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        GameObject dummy;
        dummy = gameObject;
        cam.enabled = false;
        cam.transform.position = new Vector3(0, 10, -20);
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        info = dummy.GetComponent<PlayerInfo>();
        gameObject.transform.parent = null;
        if (info.facing == -1)
        {
            traj = new Vector3(-0.05f, -0.05f, 0);
            transform.position = new Vector3(6, 35, 0);
            info.enemyScript.facing = -1;
            info.enemyScript.transform.position = new Vector3(-9, 0, 0);
        }
        else
        {
            traj = new Vector3(0.05f, -0.05f, 0);
            transform.position = new Vector3(-6, 35, 0);
            info.enemyScript.facing = 1;
            info.enemyScript.transform.position = new Vector3(9, 0, 0);

        }
        info.enemyScript.hit = -1;
        Time.timeScale = 1;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += traj;
        counter += 1;
        if (counter < 120)
        {
            if (counter % 8 < 5)
            {
                if (info.enemyScript.currentAnim != info.enemyScript.grabStun)
                {
                    info.enemyScript.currentAnim.active = false;
                    info.enemyScript.grabStun.active = true;
                    info.enemyScript.health += -1;
                }
            }
            else
            {
                if (info.enemyScript.currentAnim != info.enemyScript.GetComponent<OptionsReference>().hitDefault)
                {
                    info.enemyScript.currentAnim.active = false;
                    info.enemyScript.GetComponent<OptionsReference>().hitDefault.active = true;
                    info.enemyScript.health += -1;
                }
            }
        }
        info.enemyScript.hit = -1;
        if (info.enemyScript.nonFatal == false)
        {
            info.enemyScript.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -0.01f);
        }
        if (counter == 120)
        {
            if (info.enemyScript.nonFatal)
            {
                info.enemyScript.currentAnim.active = false;
                info.enemyScript.nonFatalAnim.active = true;
            }
            GameObject[] dummylist;
            dummylist = GameObject.Find("StageList").GetComponent<stageList>().list;
            foreach(GameObject g in dummylist)
            {
                g.active = false;
            }
            dummylist[1].active = true;
            info.transform.position = new Vector3(-6 * info.facing, 50, 0);
        }
        if (counter > 120)
        {
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -0.1f);
            if(counter < 240)
            {
                info.traj = new Vector3(0,0, 0);
            }
        }
        if(counter == 240)
        {
            info.transform.position = new Vector3(-6 * info.facing, 20, 0);
            info.GetComponent<SpriteRenderer>().enabled = true;
            info.currentAnim.active = false;
            info.victoryAnim.active = true;
        }

    }
}