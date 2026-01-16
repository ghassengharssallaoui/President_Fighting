using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joeUpBTraj : MonoBehaviour
{
    PlayerInfo info;
    public float power;
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
        float x = Mathf.Abs(info.receiver.moveVector.x);
        float y = info.receiver.moveVector.y;
        if(x < .22f)
        {
            x = .22f;
        }
        float degrees = Mathf.Atan2(1, x);

        Vector3 dummyVect = new Vector3(power * info.facing * Mathf.Cos(degrees), power * Mathf.Sin(degrees), 0);
        info.traj = dummyVect;
    }

}
