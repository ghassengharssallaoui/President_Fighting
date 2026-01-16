using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimJFKShot : MonoBehaviour
{
    PlayerInfo infoScript;
    // Start is called before the first frame update
    void Update()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        int antiPlayer;

        Rotate();
    }
    void Rotate()
    {
        string antiPlayer;
        if (infoScript.player == 1)
        {
            antiPlayer = 2+"";
        }
        else
        {
            antiPlayer = 1+"";
        }
        float num;
        Vector3 traj;
        Vector3 enemy;
        enemy = GameObject.Find("HeadsplodeLocator" + antiPlayer).transform.position;
        traj = new Vector3(transform.position.x - enemy.x, transform.position.y - (enemy.y), 0);
        float y = traj.y;
        float x = traj.x;
        num = Mathf.Atan2(traj.y, traj.x);
        transform.eulerAngles = new Vector3(0, 0, (num * 57.2f));
        if (false)
        {
            ///num = Mathf.Atan2(Mathf.Abs(y) + 0.001f / (Mathf.Abs(x) + 0.00001f));
            if (traj.y >= 0)
            {
                if (traj.x >= 0)
                {
                    print("sector1");
                    transform.eulerAngles = new Vector3(0, 0, (num));
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 45 - (num * 57.2f));
                }
            }
            else
            {

                if (traj.x >= 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 45 - (num * 57.2f));
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, -45 + (num * 57.2f));
                }
            }
        }
    }
    // Update is called once per frame

}
