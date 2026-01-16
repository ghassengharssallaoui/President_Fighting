using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyDashAttack : MonoBehaviour
{
    ReceiveInputs input;
    PlayerInfo info;
    int counter;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (input == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<PlayerInfo>();
        }
    }

        // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
