using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bidenFloat : MonoBehaviour
{
    public bidenBlastDeciderEnabler chargeScript;
    public float[] heights;
    public float totalFrames;
    public float heightDelta;
    // Start is called before the first frame update
    void OnEnable()
    {
        heightDelta = 1 * (heights[chargeScript.blastPower] - transform.position.y) / totalFrames; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (heightDelta > 0)
        {
            chargeScript.transform.position += new Vector3(0, heightDelta, 0);
        }
    }
}
