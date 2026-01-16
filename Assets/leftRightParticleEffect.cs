using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftRightParticleEffect : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    // Start is called before the first frame update
    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (left != null && right != null)
        {
            if (transform.localScale.x > 0)
            {
                left.active = true;
                right.active = false;
            }
            else
            {
                left.active = false;
                right.active = true;
            }
        }
    }
}
