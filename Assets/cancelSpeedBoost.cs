using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cancelSpeedBoost : MonoBehaviour
{
    public SpeedLimit limitScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        //limitScript.autoDash = true;
    }
    void OnDisable()
    {
        //limitScript.autoDash = false;
    }

    // Update is called once per frame
}
