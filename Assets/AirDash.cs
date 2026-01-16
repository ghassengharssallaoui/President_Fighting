using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDash : MonoBehaviour
{
    PlayerInfo info;
    public Vector3 vect;
    public float multiplier;
    public float maxWavedash;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (info == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<PlayerInfo>();
        }
        vect = new Vector3(info.receiver.moveVector.x * multiplier, info.receiver.moveVector.y * multiplier, 0);
        if (Mathf.Abs(vect.y) < 0.15f)
        {
            vect = new Vector3(vect.x, 0, 0);
        }
        if (Mathf.Abs(vect.x) < 0.15f)
        {
            vect = new Vector3(0, vect.y, 0);
        }
        
    }
    void FixedUpdate()
    {
        if(vect.y + info.transform.position.y <= 0)
        {
            float f;
            f = Mathf.Lerp(0, maxWavedash, Mathf.Abs(vect.x/multiplier));
            if(vect.x == 0)
            {
                vect = new Vector3(0.00001f, vect.y, 0);
            }
            vect = new Vector3((vect.x / Mathf.Abs(vect.x) + 0.00001f) * f, vect.y, 0);
        }
        info.traj = vect;
    }

}
