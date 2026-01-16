using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfSetSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (sr == null)
        {
            GameObject dummy;
            dummy = gameObject.transform.parent.gameObject;
            while (dummy.GetComponent<SpriteRenderer>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            sr = dummy.GetComponent<SpriteRenderer>();
        }
        sr.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    void OnDisable()
    {
        gameObject.active = false;
    }
}
