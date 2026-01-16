using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obamaBlastTrack : MonoBehaviour
{
    PlayerInfo infoScript;
    int timer;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        if (timer % 4 < 2)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = infoScript.enemyObject.transform.position;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
