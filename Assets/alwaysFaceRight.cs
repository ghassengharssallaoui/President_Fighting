using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alwaysFaceRight : MonoBehaviour
{
    public float currentScale;
    float originalx;
    // Start is called before the first frame update
    void OnEnable()
    {
        originalx = Mathf.Abs(transform.localScale.x);
        currentScale = 1;
        GameObject dummy;
        dummy = gameObject;
        while(dummy.transform.parent != null)
        {
            dummy = dummy.transform.parent.gameObject;
            currentScale = currentScale * dummy.transform.localScale.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScale > 0)
        {
            transform.localScale = new Vector3(originalx, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-originalx, transform.localScale.y, transform.localScale.z);

        }
    }
}
