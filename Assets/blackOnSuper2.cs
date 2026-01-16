using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackOnSuper2 : MonoBehaviour
{
    PlayerInfo infoScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();

    }
    void FixedUpdate()
    {
        if(infoScript.hit == 2)
        {
            GetComponent<SpriteRenderer>().color = new Vector4(0, 0, 0, 1);
        }        
        else
        {
            GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        }
    }
}
