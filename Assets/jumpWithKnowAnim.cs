using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpWithKnowAnim : MonoBehaviour
{
    PlayerInfo infoScript;
    public float jumpForce;
    public float shortHopForce;
    int counter;
    public GameObject airAnim;
    void FixedUpdate()
    {

        counter += -1;
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        if (infoScript.hit == 0)
        {
            if (infoScript.receiver.holdingJump == 2)
            {
                counter = 3;
            }
            if (counter == 0)
            {
                if (infoScript.receiver.holdingJump == 0)
                {
                    infoScript.traj += new Vector3(0, shortHopForce, 0);
                }
                else
                {
                    infoScript.traj += new Vector3(0, jumpForce, 0);
                }
                airAnim.active = true;
                airAnim.GetComponent<Options>().currentAnimFrame = GetComponent<Options>().currentAnimFrame;
                airAnim.GetComponent<Options>().currentAnim = GetComponent<Options>().currentAnim;
                airAnim.GetComponent<Options>().currentFrame = GetComponent<Options>().currentFrame;
                gameObject.active = false;
            }
        }
    }
}