using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveOnEnable : MonoBehaviour
{
    public Vector3 moveVector;
    GameObject godObject;
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
            godObject = dummy;
            infoScript = dummy.GetComponent<PlayerInfo>();
        }
        if (moveVector.x == 0 && moveVector.y == 0)//this makes the default the turnaround thing.
        {
            moveVector = new Vector3(infoScript.dashNumber - infoScript.dashSpeed, 0, 0);
        }
        godObject.transform.position += new Vector3(infoScript.facing * moveVector.x,moveVector.y,0);
    }
}
