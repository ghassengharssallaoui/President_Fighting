using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initialLoadFadeIn : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Text text;
    public int fadeTime;
    public string level;
    public int timer;
    public GameObject[] enableObjects;
    public int initialTimerDisplacement;
    public bool dontMakeImmortal;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = -30+ initialTimerDisplacement;
        if (GetComponent<SpriteRenderer>() != null)
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        }
        else
        {
            text = GetComponent<Text>();
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

        }
        if (dontMakeImmortal == false)
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    void adjustOpacity(float f)
    {
        if(sprite != null)
        {
            sprite.color += new Color(0, 0, 0, f);
        }
        else
        {
            text.color += new Color(0, 0, 0, f);
        }
    }
    void Fade()
    {

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;

        if (timer < 30 && timer > 0)
        {
            adjustOpacity( 1f / 30f);
        }
        if(timer > fadeTime + 30)
        {
            adjustOpacity( -1f / 30f);
        }
        if (timer == 30 + fadeTime)
        {
            if (enableObjects.Length > 0)
            {
                foreach (GameObject g in enableObjects)
                {
                    g.active = true;
                }
            }
            if (level != "")
            {
                Application.LoadLevel(level);
            }            
        }
        if (timer == fadeTime + 60)
        {
            Destroy(transform.root.gameObject);
        }
    }
}
