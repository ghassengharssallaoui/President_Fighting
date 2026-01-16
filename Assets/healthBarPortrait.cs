using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBarPortrait : MonoBehaviour
{
    public SpriteRenderer godSprite;
    public Vector3 startingScale;
    GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        godSprite = dummy.GetComponent<SpriteRenderer>();
        transform.SetParent(null);
        startingScale = transform.localScale;
        healthBar = GameObject.Find("Health Bar 1");
    }

    // Update is called once per frame
    void Update()
    {
        if (godSprite != null)
        {
            float scale = Mathf.Abs(startingScale.x);
            if (godSprite.GetComponent<PlayerInfo>().player != 1 )
            {
                transform.localScale = new Vector3(-scale * healthBar.transform.localScale.x, scale * healthBar.transform.localScale.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(scale * healthBar.transform.localScale.x, scale * healthBar.transform.localScale.y, 1);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        GetComponent<SpriteRenderer>().color = godSprite.color;

    }
}
