using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitActivate : MonoBehaviour
{
    public portraitSlideIn script;
    public Sprite playerSprite;
    public int player;
    SpriteRenderer spr;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (script.GetComponent<SpriteRenderer>().sprite != GetComponent<SpriteRenderer>().sprite)
        {
            if (player == 1)
            {
                if(spr == null)
                {
                    spr = GameObject.Find("P1Character").GetComponent<SpriteRenderer>();
                }
                script.transform.position = new Vector3(-20, transform.position.y, 0);
            }
            else
            {
                if (spr == null)
                {
                    spr = GameObject.Find("P2Character").GetComponent<SpriteRenderer>();
                }
                script.transform.position = new Vector3(20, transform.position.y, 0);

            }
            spr.sprite = playerSprite;
            script.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            script.transform.localScale = transform.localScale;
            script.target = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
