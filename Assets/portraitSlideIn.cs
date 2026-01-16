using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portraitSlideIn : MonoBehaviour
{
    public Vector3 target;
    public int posNegative;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = -(transform.position.x - target.x) / 3;
        if(Mathf.Abs(x) < 0.008f)
        {
            transform.position = target;
        }
        transform.position += new Vector3(-(transform.position.x - target.x)/3, 0, 0);
    }
}
