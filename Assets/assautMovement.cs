using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class assautMovement : MonoBehaviour
{
    PlayerInfo info;
    public Vector3 vector;
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
        float dir;
        dir = info.gameObject.transform.position.x - info.enemyObject.transform.position.x;
        dir = dir / Mathf.Abs(dir) * -1;
        info.facing = (int)dir;
        info.traj = new Vector3 (vector.x * dir, vector.y, 0);
    }
}
