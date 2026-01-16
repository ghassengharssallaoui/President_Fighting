using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stupidCamera : MonoBehaviour
{
    public int counter;
    float floatCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 20;
        counter += 1;
        if(counter % 200 > 100) 
        {
            transform.position += new Vector3(3f, 0, 0);
        }
        else
        {
            transform.position += new Vector3(-3f, 0, 0);
        }
    }
}
