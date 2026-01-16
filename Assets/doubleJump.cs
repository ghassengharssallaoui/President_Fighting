using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJump : MonoBehaviour
{
    PlayerInfo infoScript;
    int timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
        if (infoScript == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }

            infoScript = dummy.GetComponent<PlayerInfo>();
        }
            //do doublejump stuff, like an animation, etc.

            //the following bullshit makes it harder to change your momentum with a double jump UNIVERSALLY. Be prepared to deal with this later.
        float x = infoScript.receiver.moveVector.x * infoScript.airSpeed;
        if ((x > 0 && infoScript.traj.x > 0) || (x < 0 && infoScript.traj.x < 0))
        {
            if (Mathf.Abs(x) > infoScript.airSpeed)
            {
                x = infoScript.airSpeed * (x / Mathf.Abs(x) + 0.00001f);
            }
        }
        else
        {
            x = 0;
        }
        infoScript.traj = new Vector3(x, infoScript.doubleJumpForce, 0);
        
    }
    void FixedUpdate()
    {
        if(timer < 0)//change if you wanna bring back the boost on dj
        {
            infoScript.gameObject.transform.position += new Vector3(0, infoScript.jumpForce * 0.5f, 0);
        }
        timer += 1;
    }
}
