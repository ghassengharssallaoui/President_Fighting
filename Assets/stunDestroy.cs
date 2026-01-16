using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stunDestroy : MonoBehaviour
{
    PlayerInfo info;
    // Start is called before the first frame update
    void OnEnable()
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
        if (info.hit != -3)//If anyone's wondering why we're doing this here, it's just in case both people are stunned at the same time.
        {
            if(cam.p1.GetComponent<PlayerInfo>().hit == -3)
            {
                info = cam.p1.GetComponent<PlayerInfo>();
            }
            else
            {
                info = cam.p2.GetComponent<PlayerInfo>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(info.hit != -3)
        {
            Destroy(gameObject);
        }
    }
}
