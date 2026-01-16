using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oswald : MonoBehaviour
{
    public PlayerInfo parentInfo;
    public PlayerInfo infoScript;
    public float leftRight;
    public bool done;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (done == false)
        {
            
            done = true;
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            infoScript = dummy.GetComponent<PlayerInfo>();
            dummy = gameObject.transform.parent.gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            parentInfo = dummy.GetComponent<PlayerInfo>();
            float dummyx;
            BetterCameraMovement cam;
            cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
            if (transform.position.x < parentInfo.enemyObject.transform.position.x)
            {
                dummyx = cam.transform.position.x + cam.gameObject.transform.position.z * cam.realityRatio;
                leftRight = 1;
                infoScript.facing = -1;
            }
            else
            {
                dummyx = cam.transform.position.x - cam.gameObject.transform.position.z * cam.realityRatio;
                leftRight = -1;
                infoScript.facing = 1;
            }
            float dummyCamFloat;
            dummyCamFloat = (Screen.width/Screen.height) / (16f / 9f);
            infoScript.gameObject.transform.position = new Vector3(dummyx * dummyCamFloat, infoScript.gameObject.transform.position.y + 5, 0);
            infoScript.traj = new Vector3(0.3f * leftRight, 0.1f, 0);
            infoScript.currentAnim.active = false;
            infoScript.GetComponent<OptionsReference>().airDefault.active = true;
            if (parentInfo.hit != 2)
            {
                Destroy(gameObject);
            }
            transform.SetParent(null);
            if(parentInfo.GetComponent<SpriteRenderer>().color == Color.red)
            {
                GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
    void FixedUpdate()
    {
        float dummyx;
        print(Time.timeScale);
        //infoScript.gameObject.transform.position = new Vector3(dummyx, infoScript.gameObject.transform.position.y + 5, 0);
        if (infoScript.traj.x == 0 && transform.position.y != 0)
        {
            infoScript.gameObject.transform.position += new Vector3(0.3f * leftRight, infoScript.traj.y, 0);
            infoScript.traj += new Vector3(0, -infoScript.fallSpeed, 0);
        }
        //infoScript.currentAnim.active = false;
        //infoScript.GetComponent<OptionsReference>().airDefault.active = true;
        //if (parentInfo.hit != 2)
        // {
        //  Destroy(gameObject);
        //}
    }
    void LateUpdate()
    {
        if (parentInfo == null)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < parentInfo.enemyObject.transform.position.x)
        {
            //dummyx = cam.transform.position.x + cam.gameObject.transform.position.z * cam.realityRatio;
            leftRight = 1;
            infoScript.facing = -1;
            transform.localScale = new Vector3(-1, 1, 1);

        }
        else
        {
            //dummyx = cam.transform.position.x - cam.gameObject.transform.position.z * cam.realityRatio;
            leftRight = -1;
            infoScript.facing = 1;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Update is called once per frame

}

