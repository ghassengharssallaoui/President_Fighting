using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimBullet : MonoBehaviour
{
    PlayerInfo infoScript;
    GameObject parent;
    // Start is called before the first frame update
    void Update()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        parent = dummy;
        infoScript = dummy.GetComponent<PlayerInfo>();
        ////
        ///
        Rotate();
        int dummyint;
        float dummyFloat;
        dummyFloat = -1 * (int)(Mathf.Abs(infoScript.transform.position.x - infoScript.enemyObject.transform.position.x) / (infoScript.transform.position.x - infoScript.enemyObject.transform.position.x));
        if(dummyFloat > 0)
        {
            dummyint = 1;
        }
        else
        {
            dummyint = -1;
        }
        parent.transform.localScale = new Vector3(dummyint, 1, 1);
        infoScript.facing = dummyint;
    }


    void Rotate()
    {
        float num;
        Vector3 traj;
        Vector3 enemy;
        enemy = infoScript.enemyObject.transform.position;
        traj = new Vector3(transform.position.x - enemy.x, transform.position.y - (enemy.y + 3), 0);
        float y = traj.y;
        float x = traj.x;
        num = Mathf.Atan2(traj.y, traj.x);
        if (parent.transform.localScale.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 180 +(num * 57.2f));
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, (num * 57.2f));

        }
        ///
    }
}
