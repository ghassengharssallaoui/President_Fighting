using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSprite : MonoBehaviour //eventually make facing happen on this step as well.
{
    SpriteRenderer sr;
    PlayerInfo infoScript;
    public GameObject godObject;
    public float height;
    public float alpha;
    public string sortingLayer;
    bool doit;
    // Start is called before the first frame update
    void OnEnable()
    {
        if(sr == null)
        {
            if(alpha == 0)
            {
                alpha = 1;
            }
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            infoScript = dummy.GetComponent<PlayerInfo>();
            godObject = dummy;
            sr = godObject.GetComponent<SpriteRenderer>();
        }
        doit = true;
    }
    void Update()
    {
        if (doit)//this makes it so the animation always happens after any other OnEnable scripts on the object
        {
            if (height == 0)
            {
                infoScript.height = infoScript.defaultHeight;
            }
            else
            {
                infoScript.height = height;
            }
            if (sortingLayer == "")
            {

                sortingLayer = "Default";
            }

            sr.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            sr.color = new Vector4(sr.color.r, sr.color.g, sr.color.b, alpha);
            if (godObject.name != "Oswald")
            {
                godObject.transform.localScale = new Vector3(infoScript.facing, 1, 1);
            }
            sr.sortingLayerName = sortingLayer;
            doit = false;
        }

    }
}
