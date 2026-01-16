using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findPlayer : MonoBehaviour
{
    public Vector3 displacementFromPlayer;
    BetterCameraMovement cam;
    public PlayerInfo player;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (player == null)
        {
            if (cam == null)
            {
                cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
            }
            float x1;
            float x2;
            x1 = Mathf.Abs(cam.p1.transform.position.x - transform.position.x);
            x2 = Mathf.Abs(cam.p2.transform.position.x - transform.position.x);
            if (x1 < x2)
            {
                player = cam.p1.GetComponent<PlayerInfo>();
            }
            else
            {
                player = cam.p2.GetComponent<PlayerInfo>();
            }
        }
    }
}
