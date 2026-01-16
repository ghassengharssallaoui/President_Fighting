using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joeExAnimationFlash : MonoBehaviour
{
    public GameObject[] flashAnims;
    public PlayerInfo info;
    SpriteRenderer spr;
    Color baseColor;
    int timer;
    public int flashSpeed;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        info = GetComponent<PlayerInfo>();
        if (info.characterID == info.enemyScript.characterID && info.player == 2) {
            baseColor = Color.red;
        } else 
        {
            baseColor = Color.white;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        bool doit = false;
        foreach (GameObject g in flashAnims)
        {
            if (g.active)
            {
                doit = true;
            }
        }
        if (doit)
        {
            if(timer % flashSpeed < flashSpeed / 2)
            {
                spr.color = Color.yellow;
            }
            else
            {
                spr.color = baseColor;
            }
        }
        else
        {
            spr.color = baseColor;
        }
    }
}
