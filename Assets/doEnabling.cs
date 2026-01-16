using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doEnabling : MonoBehaviour
{
    public bigEnabler BigEnable;
    // Start is called before the first frame update
    void OnEnable()
    {
        BigEnable.doEnable = true;
        gameObject.active = false;
        gameObject.active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
