using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBoxAssigner : MonoBehaviour
{
    public GameObject[] hitBoxes;
    public PlayerInfo info;
    public int player;
    public bool basic = true;
    // Start is called before the first frame update
    void Start()
    {
        player = info.player;
        if (basic)
        {
            foreach (GameObject g in hitBoxes)
            {
                if (player == 2)
                {
                    g.GetComponent<hitbox>().p1collide = true;
                    g.GetComponent<hitbox>().p1default = true;
                }
                else
                {
                    g.GetComponent<hitbox>().p2collide = true;
                    g.GetComponent<hitbox>().p2default = true;
                }
                g.GetComponent<HitBoxInterpreter>().info = info;
            }
        }
    }

    // Update is called once per frame
    
}
