using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteFadeOut : MonoBehaviour
{

    public float frames;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, gameObject.GetComponent<SpriteRenderer>().color.a - 1 / frames);
    }
}
