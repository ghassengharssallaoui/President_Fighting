using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingtonImageSpammer : MonoBehaviour
{
    public GameObject sfx;
    public GameObject sprite;
    public int timer;
    int counter;
    public Sprite[] sprites;
    public float[] spritesScale;
    public AudioClip[] audio;
    GameObject[] killerArray = new GameObject[100];
    // Update is called once per frame
    void FixedUpdate()
    {
        timer += 1;
        if (timer < 90)
        {
            if (timer % 20 == 5)
            {
                GameObject g;
                g = Instantiate(sfx);
                g.GetComponent<AudioSource>().clip = audio[Random.Range(0, audio.Length - 1)];
                g.GetComponent<AudioSource>().Play();
            }
        }
        if (timer < 100)
        {
            if (timer % 5 == 4)
            {
                GameObject g;
                g = Instantiate(sprite);
                g.transform.position = new Vector3(Random.Range(-35, 35), Random.Range(0, 30), 0);
                g.transform.eulerAngles = new Vector3(0, 0, Random.Range(-30, 30));
                int i;
                i = Random.Range(0, sprites.Length - 1);
                g.GetComponent<SpriteRenderer>().sprite = sprites[i];
                g.transform.localScale = new Vector3(spritesScale[i] *0.8f, spritesScale[i]*0.8f, spritesScale[i]);
                killerArray[counter] = g;
                counter += 1;
            }
        }
        if (timer == 130)
        {
            foreach(GameObject g in killerArray)
            {
                if (g != null)
                {
                    g.active = false;
                }
            }
            gameObject.active = false;
        }
    }
}
