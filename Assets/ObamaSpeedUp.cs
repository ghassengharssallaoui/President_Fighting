using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObamaSpeedUp : MonoBehaviour
{
    public PlayerInfo player;
    public string[] validAnims;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<findPlayer>().player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool doit;
        doit = false;
        foreach(string s in validAnims)
        {
            if(s == player.currentAnim.name)
            {
                doit = true;
            }
        }
        if (doit && player.hit == 0)
        {
            player.transform.position += new Vector3(player.traj.x * 0.65f, 0, 0);
        }
    }
}
