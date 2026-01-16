using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bidenBlackCinematicEffect : MonoBehaviour
{
    public float fadeOutTime;
    public float fadeInTime;
    float updateTimer;
    public bool outReady;
    float inDelta;
    float outDelta;
    // Start is called before the first frame update
    void Start()
    {
        inDelta = 1.1f / fadeInTime;
        outDelta = 1 / fadeOutTime;
    }
    void FixedUpdate()
    {
        if (fadeInTime >= 0)
        {
            fadeInTime += -1;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color += new Color(0, 0, 0, inDelta);
        }
        else
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.color += new Color(0, 0, 0, -outDelta);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0.00001f && Time.deltaTime < 0.0001f)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= 0.00001f / 60f)
            {
                updateTimer += -0.00001f / 60f;
                FixedUpdate();
            }
        }
    }
}
