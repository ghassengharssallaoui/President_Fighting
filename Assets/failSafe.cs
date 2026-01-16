using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class failSafe : MonoBehaviour
{
    public GameObject[] anims;
    public GameObject airIdle;
    public GameObject groundIdle;
    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        int dummy;
        dummy = 0;
        for(int i = 0; i < anims.Length; i++)
        {
            if (anims[i].active == true)
            {
                dummy += 1;
            }
            
        }
        if(dummy == 0)
        {
            if(transform.position.y > 0)
            {
                airIdle.active = true;
            }
            else
            {
                groundIdle.active = true;
            }
        }
    }
}
