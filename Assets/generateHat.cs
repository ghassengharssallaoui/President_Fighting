using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generateHat : MonoBehaviour
{
    public GameObject hat;
    PlayerInfo infoScript;
    public GameObject dontSpawnFrame;
    public float bonusFrames;
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
    }
    void OnDisable()
    {
        bool mybool = true;
        if(GetComponent<Options>().air == false && infoScript.transform.position.y != 0)
        {
            mybool = false;
        }
        if (dontSpawnFrame.active == false && infoScript.hit == 0 && mybool && infoScript.receiver.holdingSpecial == 0)
        {
            GameObject dummy;
            GameObject g;
            g = hat;
            dummy = Instantiate(g, new Vector3(infoScript.transform.position.x, infoScript.transform.position.y + g.transform.position.y, 0), Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
            dummy.transform.position += new Vector3(g.transform.position.x * infoScript.facing, 0, 0);//The reason we do this second is so that (f
            dummy.transform.localScale = new Vector3(dummy.transform.localScale.x * infoScript.facing, dummy.transform.localScale.y, dummy.transform.localScale.z);
            if (dummy.GetComponent<findPlayer>() != null)
            {
                dummy.GetComponent<findPlayer>().player = infoScript;
            }
            float multiplier = 1 + (GetComponent<Options>().currentFrame + bonusFrames) / 60f;
            dummy.GetComponent<projectileTraj>().accel = new Vector3(dummy.GetComponent<projectileTraj>().accel.x * multiplier, 0, 0);
            dummy.GetComponent<projectileTraj>().traj = new Vector3(dummy.GetComponent<projectileTraj>().traj.x * multiplier, 0, 0);
            dummy.GetComponent<hitBoxAssigner>().info = infoScript;
            print(dummy.GetComponent<projectileTraj>().accel);
        }
    }
        
}
