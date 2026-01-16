using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumpSquat : MonoBehaviour
{
    public Vector3 vector;
    public jumpSquat js;
    PlayerInfo infoScript;
    // Start is called before the first frame update
    void OnEnable()
    {
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
        if (transform.position.y == 0 && infoScript.receiver.holdingJump <= 0)
        {
            infoScript.traj = new Vector3(js.vector.x, infoScript.shortHopForce, 0);
        }
        else
        {
            infoScript.traj = js.vector;
        }
        
    }
}
