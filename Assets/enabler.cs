using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabler : MonoBehaviour
{
    public GameObject[] enable;
    public GameObject[] disable;
    public GameObject[] instant;
    public PlayerInfo playerInfoForFacing;
    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (GameObject g in enable)
        {
            g.active = true;
        }
        foreach (GameObject g in disable)
        {
            g.active = false;
        }
        foreach (GameObject g in instant)
        {
            GameObject dummy;
            int facing;
            if(playerInfoForFacing == null)
            {
                facing = 1;
            }
            else
            {
                facing = playerInfoForFacing.facing;
            }
            dummy = Instantiate(g, new Vector3(g.transform.position.x * facing + transform.position.x, transform.position.y + g.transform.position.y, 0), Quaternion.identity);
            dummy.transform.localScale = new Vector3(g.transform.localScale.x * facing, g.transform.localScale.y, 1);
        }
    }
}
