using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lincolnTurnaroundOrPunchDecider : MonoBehaviour
{
    PlayerInfo infoScript;
    public GameObject turnAroundAnim;
    public GameObject punch;
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
        if((infoScript.facing == 1 && infoScript.receiver.holding[2] > 0) || (infoScript.facing == -1 && infoScript.receiver.holding[3] > 0))
        {
            turnAroundAnim.active = true;
        }
        else
        {
            punch.active = true;
        }
        gameObject.active = false;
    }

}
