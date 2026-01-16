using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMenuOption : MonoBehaviour
{
    public bool hovering;
    public VerticalMenu myMenu;
    public GameObject[] hoverObjects;
    public int aPress; //0 = not activated. 1 = pressed once.
    public int bPress;
    public int lPress;
    public int rPress;
    public bool dontZeroOnLeave;
    public bool dontDisableOnDisable;
    void Update()
    {
        if (hovering)
        {
            foreach(GameObject g in hoverObjects)
            {
                if(g.active == false)
                {
                    g.active = true;
                }
            }
        }
        else
        {
            foreach (GameObject g in hoverObjects)
            {
                g.active = false;
            }
            if(dontZeroOnLeave == false)
            {
                aPress = 0;
                bPress = 0;
                lPress = 0;
                rPress = 0;
            }
        }
    }
    void OnDisable()
    {
        if (dontDisableOnDisable == false)
        {
            foreach (GameObject g in hoverObjects)
            {
                g.active = false;
            }
        }
        if (dontZeroOnLeave == false)
        {
            aPress = 0;
            bPress = 0;
            lPress = 0;
            rPress = 0;
        }
    }
}
