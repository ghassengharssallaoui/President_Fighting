using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p1SelectorThrobber : MonoBehaviour
{
    SpriteRenderer spr;
    public bool p1;
    public bool p2;
    public bool p1Selected;
    public bool p2Selected;
    public int selectCounter;
    public int throbCounter;
    public float throbSpeed;
    int speedThrobCounter;
    static int universalThrobCounter;
    static float universalAlpha;
    public GameObject cpu;
    // Start is called before the first frame update
    void OnEnable()
    {    
        spr = GetComponent<SpriteRenderer>();
    }
    void ThrobBack()
    {
        if (spr.color.a > 1)
        {
            spr.color = new Vector4(spr.color.r, spr.color.g, spr.color.b, 1);
        }
        spr.color = new Vector4(spr.color.r, spr.color.g, spr.color.b, spr.color.a - 1.5f / throbSpeed);

    }
    void ThrobForward()
    {
        if(spr.color.a < 0.25f)
        {
            spr.color = new Vector4(spr.color.r, spr.color.g, spr.color.b, 0.25f);
        }
        spr.color = new Vector4(spr.color.r, spr.color.g, spr.color.b, spr.color.a + 1.5f / throbSpeed);
    }
    void Throb()
    {
        throbCounter += 1;
        if(throbCounter % throbSpeed > throbSpeed / 2)
        {
            ThrobForward();
        }
        else
        {
            ThrobBack();
        }
    }
    void UniversalThrob()
    {
        universalThrobCounter += 1;
        
        if (universalThrobCounter % throbSpeed > throbSpeed / 2)
        {
            if (universalAlpha < 0.25f)
            {
                universalAlpha = 0.25f;
            }
            
            
            universalAlpha += 1.5f / throbSpeed;

        }
        else
        {
            if (universalAlpha > 1)
            {
                
                universalAlpha = 1;
            }
            universalAlpha += -1.5f / throbSpeed;

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (cpu.active == true)//if you're wondering what this is for. So am I. Some fucking script is turning on this shit when it's not supposed to so I'm hard coding it out.
        {
            p2 = false;
        }
        if (p1Selected || p2Selected)
        {
            if (p1Selected)
            {
                p1 = true;
            }
            if (p2Selected)
            {
                p2 = true;
            }
        }
        if (p1)
        {
            UniversalThrob();
        }
        if (selectCounter > 0)
        {
            if(selectCounter == 1)
            {
                throbCounter = Mathf.FloorToInt(universalThrobCounter % throbSpeed);
            }
            selectCounter += 1;
            if (selectCounter < throbSpeed)
            {
                Throb();
                Throb();
                Throb();
                Throb();
                Throb();
            }
            else
            {
                Throb();
                Throb();
                Throb();
                Throb();
                Throb();
                ThrobForward();
                if (spr.color.a >= 1)
                {
                    selectCounter = 0;
                }
            }
        }
        else if (p1Selected || p2Selected)
        {
            if (p1 && p2)
            {
                spr.color = new Vector4(0.5f, 0, 1, 1);
            }
            else if (p1Selected)
            {
                p1 = true;
                spr.color = new Vector4(1, 0, 0, 1);

            }
            else if (p2Selected)
            {
                p2 = true;
                spr.color = new Vector4(0, 0, 1, 1);

            }
            spr.color = new Vector4(spr.color.r, spr.color.g, spr.color.b, 1);
        }
        else if (p1 || p2)
        {

            if (p1 && p2)
            {
                spr.color = new Vector4(0.5f, 0, 1, universalAlpha);
            }
            else if (p1)
            {
                spr.color = new Vector4(1, 0, 0, universalAlpha);
            }
            else
            {
                spr.color = new Vector4(0, 0, 1, universalAlpha);
            }
        }
        else
        {
            spr.color = Color.black;
        }
        
    }
}
