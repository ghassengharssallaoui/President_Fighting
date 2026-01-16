using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockSuper : MonoBehaviour
{
    PlayerInfo infoScript;
    GameObject godObject;
    public bool value;
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
        godObject = dummy;
        infoScript.blockSuper = value;
    }
}
