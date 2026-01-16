using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingtonSuperWiffOrHit : MonoBehaviour
{
    public GameObject super;
    PlayerInfo infoScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        if(infoScript.enemyScript.hit == -2)
        {
            infoScript.currentAnim = super;
            infoScript.gameObject.transform.position = new Vector3(infoScript.enemyScript.gameObject.transform.position.x, 50, 0);
            infoScript.traj = new Vector3(0, 0, 0);
        }
        else
        {
            float dummyx;
            float leftRight;
            BetterCameraMovement cam;
            cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
            if (transform.position.x > 0)
            {
                dummyx = cam.transform.position.x + cam.gameObject.transform.position.z * cam.realityRatio;
                leftRight = 1;
            }
            else
            {
                dummyx = cam.transform.position.x - cam.gameObject.transform.position.z * cam.realityRatio;
                leftRight = -1;
            }

            infoScript.gameObject.transform.position = new Vector3(dummyx, infoScript.gameObject.transform.position.y +0.01f, 0);
            infoScript.traj = new Vector3(0.3f * leftRight, 0.1f, 0);
            infoScript.currentAnim.active = false;
            infoScript.GetComponent<OptionsReference>().airDefault.active = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(infoScript.hit < 0)
        {
            //This is an empty if that I'm writing to attempt to fix a glitch. tbh, idk why the second if even needs to exist anymore but if this works it works.
        }
        else if (infoScript.enemyScript.hit != -2)
        {
            infoScript.hit = 0;
        }
        gameObject.active = false;
    }
}
