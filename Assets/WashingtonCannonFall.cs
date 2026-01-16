using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingtonCannonFall : MonoBehaviour
{
    public int liveFrames;
    public GameObject[] hitboxes;
    public bool notDupe;
    // Start is called before the first frame update
    void DeleteDupes()
    {
        if (GameObject.Find("WashingtonCannonball" + GetComponent<hitBoxAssigner>().player + "1") == null)//remove the true if you wanna bring back limiting cannonballs
        {
            name = "WashingtonCannonball" + GetComponent<hitBoxAssigner>().player +"1";
            GetComponent<SpriteRenderer>().enabled = true;
            foreach (GameObject g in hitboxes)
            {
                g.active = true;
            }
        }else if (GameObject.Find("WashingtonCannonball" + GetComponent<hitBoxAssigner>().player + "2") == null)//remove the true if you wanna bring back limiting cannonballs
        {
            name = "WashingtonCannonball" + GetComponent<hitBoxAssigner>().player+"2";
            GetComponent<SpriteRenderer>().enabled = true;
            foreach (GameObject g in hitboxes)
            {
                g.active = true;
            }
        }
        else
        {
            gameObject.active = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(GetComponent<hitBoxAssigner>().player != 0 && notDupe == false)
        {
            DeleteDupes();
            notDupe = true;
        }
        liveFrames += -1;
        if(liveFrames <= 0)
        {
            GetComponent<wobble>().enabled = false;
            GetComponent<projectileTraj>().traj += new Vector3(0,-0.1f,0);

            if (transform.position.y <= 0)
            {
                foreach (GameObject g in hitboxes)
                {
                    g.active = false;
                }
                name = "";
                transform.position = new Vector3(transform.position.x, 0, 0);
                GetComponent<projectileTraj>().enabled = false;
                GetComponent<JustAnimate>().enabled = false;
                GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);
            }
        }
       
    }
}
