using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraTrack : MonoBehaviour
{
    PlayerInfo info;
    Vector3 camPosition;
    Vector3 traj;
    int startingxDirection;
    int startingyDirection;
    public bool returnToCam;
    // Start is called before the first frame update
    void OnEnable()
    {
        info = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>().p1.GetComponent<PlayerInfo>();//this is backwards as fuck because I copy and pasted the code from the lincoln coin script
        camPosition = info.cam.transform.position;
        AddToCam();
        if(camPosition.x - transform.position.x >= 0)
        {
            startingxDirection = 1;
        }
        else
        {
            startingxDirection = -1;
        }
        if (camPosition.y - transform.position.y >= 0)
        {
            startingyDirection = 1;
        }
        else
        {
            startingyDirection = -1;
        }
    }
    void AddToCam()
    {
        for (int i = 0; i < info.cam.otherOnScreenObjects.Length; i++)
        {
            if (info.cam.otherOnScreenObjects[i] == null)
            {
                info.cam.otherOnScreenObjects[i] = gameObject;
                i = 10000;
            }
        }
    }
    void RemoveFromCam()
    {
        for (int i = 0; i < info.cam.otherOnScreenObjects.Length; i++)
        {
            if (info.cam.otherOnScreenObjects[i] != null)
            {
                if (info.cam.otherOnScreenObjects[i] == gameObject)
                {
                    info.cam.otherOnScreenObjects[i] = null;
                }
            }
        }
    }
    void FixedUpdate()
    {
        camPosition = info.cam.transform.position;

        if (returnToCam)
        {
            traj += new Vector3(0.025f * startingxDirection, 0.025f * startingyDirection, 0);
            Vector3 trueTraj;
            trueTraj = traj;
            if ((traj.x + transform.position.x > camPosition.x && startingxDirection == 1) || (traj.x + transform.position.x < camPosition.x && startingxDirection == -1))
            {
                trueTraj = new Vector3(0, trueTraj.y, 0);
                transform.position = new Vector3(camPosition.x, transform.position.y, 0);
            }
            if ((traj.y + transform.position.y > camPosition.y && startingyDirection == 1) || (traj.y + transform.position.y < camPosition.y && startingyDirection == -1))
            {
                trueTraj = new Vector3(trueTraj.x, 0, 0);
                transform.position = new Vector3(transform.position.x, camPosition.y, 0);
            }
            if(trueTraj == new Vector3(0, 0, 0))
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position += trueTraj;
            }
        }
        //if()
    }
    // Update is called once per frame
    void OnDisable()
    {
        RemoveFromCam();
    }
}
