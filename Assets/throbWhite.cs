using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throbWhite : MonoBehaviour
{
    SpriteRenderer sprite;
    public int counter;
    int counter2;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        counter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        counter2 += 1;
        if (counter2 % 4 == 0)
        {
            counter += 1;
        }
        if (counter < 30)
        {
            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f+(60 - (counter * 2)) /360f);
        }
        if(counter > 30)
        {
            sprite.color = new Vector4(sprite.color.r, sprite.color.g, sprite.color.b, 0.3f+((counter  -30) * 2) / 360f);
        }
        if(counter > 60)
        {
            counter = 1;
        }
    }
}
