using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreKeeper : MonoBehaviour
{
    public PlayerInfo info;
    public BetterCameraMovement camScript;
    public int player;
    public GameObject[] ultPointIndicators;
    public GameObject[] healthIndicators;
    int score;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player == 1)
        {
            if(camScript.p1 != null)
            {
                info = camScript.p1.GetComponent<PlayerInfo>();
            }
        }
        else
        {
            if (camScript.p2 != null)
            {
                info = camScript.p2.GetComponent<PlayerInfo>();
            }
        }
        if(info.superCharge >= info.superCost)
        {
            foreach(GameObject g in ultPointIndicators)
            {
                g.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 1);
            }
        }
        else
        {
            foreach (GameObject g in ultPointIndicators)
            {
                g.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
            }
        }
        for(int i = 0; i < ultPointIndicators.Length; i++)
        {
            if(i < info.superCharge && i < info.superCost)
            {
                ultPointIndicators[i].active = true;
            }
            else
            {
                ultPointIndicators[i].active = false;
            }
        }
        for (int i = 0; i < healthIndicators.Length; i++)
        {
            if (i < info.health)
            {
                healthIndicators[i].active = true;
            }
            else
            {
                healthIndicators[i].active = false;
            }
        }
    }
}
