using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiScaleWithAspectRatio : MonoBehaviour
{
    public bool noMovingCam;
    Vector3 ogScale;
    public Vector3 truePosition;
    public Vector3 trueScale;
    public GameObject cam;
    public float trueZ;
    public float trueY;
    public bool onlyY;
    public bool alwaysResizeForOnline;
    public bool scaleOnly;
    public GameObject parent;
    public bool nullParent;
    void OnEnable()
    {
        if (noMovingCam)
        {
            if (ogScale == new Vector3(0, 0, 0))
            {
                ogScale = transform.localScale;
            }
            nullParent = true;
        }
        else
        {
            if (truePosition == new Vector3(0, 0, 0) || alwaysResizeForOnline) //makes it only happen the first time
            {
                if (transform.parent == null)
                {
                    nullParent = true;
                }
                else
                {
                    parent = transform.parent.gameObject;
                }
                    truePosition = transform.position;
                trueScale = transform.localScale;
                cam = GameObject.Find("Main Camera");
                trueZ = transform.position.z - cam.transform.position.z;
                trueY = -transform.position.y + cam.transform.position.y;
                if (scaleOnly == false)
                {
                    transform.SetParent(cam.transform);
                }
            }
        }
        LateUpdate();

    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void LateUpdate()
    {
        if (nullParent == false)
        {
            if (parent == null)
            {
               Destroy(gameObject);
            }
            else
            {
                if (parent.active == false)
                {
                    transform.SetParent(parent.transform);
                }
            }
        }
        float screenheight = Screen.height;
        float screenwidth = Screen.width;
        float bastardRes = screenwidth / screenheight;//bastard because i'm pissed I even have to do this
        float aspectMultiplier = 1.7777f / bastardRes;
        if (noMovingCam)
        {
            float defaultAsp = 16f / 9f;
            float floatHeight = Screen.height;
            float floatWideth = Screen.width;
            float aspectRatio = floatWideth / floatHeight;
            transform.localScale = new Vector3(ogScale.x * aspectRatio / defaultAsp, ogScale.y, 1);
        }
        else
        {
            if (onlyY == false)
            {
                transform.position = new Vector3(transform.position.x, cam.transform.position.y - trueY * aspectMultiplier, cam.transform.position.z + trueZ * aspectMultiplier);
                transform.localScale = new Vector3(trueScale.x, trueScale.y * aspectMultiplier, trueScale.z * aspectMultiplier);
            }
            else
            {
                transform.localScale = new Vector3(trueScale.x * aspectMultiplier, trueScale.y * aspectMultiplier, trueScale.z * aspectMultiplier);
            }
        }
    }
}
