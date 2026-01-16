using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxSoundGenerator : MonoBehaviour
{
    public string soundID;
    BetterCameraMovement camScript;
    public PlayerInfo p1;
    public PlayerInfo p2;
    int p1last;
    int p2last;
    public bool hitWait; //this just makes it so it doesn't happen more than once;
    public GameObject strongHit;
    public GameObject weakHit;
    float updateTimer;
    // Start is called before the first frame update
    void Start()
    {
        camScript = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
    }

    // Update is called once per frame
    void PlaySound(int i)
    {
        if(soundID == "No Sound")
        {
            //
        }
        ///////////the general ones
        else if(i == -1)
        {
            Instantiate(strongHit);
        }
        else if(i == -3)
        {
            Instantiate(weakHit);
        }
    }
    void Update()
    {
        if (camScript.p1 != null)
        {
            p1 = camScript.p1.GetComponent<PlayerInfo>();
            p2 = camScript.p2.GetComponent<PlayerInfo>();
        }
        if (p1.hit != p1last)
        {
            p1last = p1.hit;
            PlaySound(p1.hit);
        }
        if (p2.hit != p2last)
        {
            p2last = p2.hit;
            PlaySound(p2.hit);
        }
    }
}
