using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPlayerToName : MonoBehaviour
{
    PlayerInfo infoScript;
    int counter;
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

    // Update is called once per frame
    void Update()
    {
        counter += 1;
        if(counter == 2)
        {
            gameObject.name += "" + infoScript.player;
        }
    }
}
