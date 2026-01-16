using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleJumpDontRenderOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        if(transform.position.y >= 0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
