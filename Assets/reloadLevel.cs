using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadLevel : MonoBehaviour
{
    int counter;
    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if(counter > 120)
        {
            Application.LoadLevel("TestScene");
        }
    }
}
