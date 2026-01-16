using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nonLethalAnimChanger : MonoBehaviour
{
    public GameObject[] lethalObjs;
    public GameObject[] nonLethalObjs;

    bool nonLethal;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        nonLethal = dummy.GetComponent<PlayerInfo>().enemyScript.nonFatal;
        if (nonLethal == false)
        {
            foreach (GameObject g in lethalObjs)
            {
                g.active = true;
            }
        }
        else
        {
            foreach (GameObject g in nonLethalObjs)
            {
                g.active = true;
            }
        }

        gameObject.active = false;
    }
    // Start is called before the first frame update
}
