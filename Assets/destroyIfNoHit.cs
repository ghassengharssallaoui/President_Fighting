using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyIfNoHit : MonoBehaviour
{
    public PlayerInfo info;
    // Start is called before the first frame update
    void Start()
    {
        BetterCameraMovement cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        if (Mathf.Abs(cam.p1.transform.position.x - transform.position.x) > Mathf.Abs(cam.p2.transform.position.x - transform.position.x))
        {
            info = cam.p2.GetComponent<PlayerInfo>();
        }
        else
        {
            info = cam.p1.GetComponent<PlayerInfo>();
        }
        if(info.hit != -1)
        {
            print(info.player);
            Destroy(gameObject);
        }
    }
}
