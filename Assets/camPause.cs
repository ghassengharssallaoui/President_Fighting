using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camPause : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>().pauseOnPause = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
