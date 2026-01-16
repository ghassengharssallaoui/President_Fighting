using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obamaCinemaBars : MonoBehaviour
{
    GameObject cam;
    public float ypos;
    public int ifTopPutPositive1;
    public int timer;
    public float updateTimer;
    // Start is called before the first frame update
    void OnEnable()
    {
        cam = GameObject.Find("Main Camera");
        ypos = 18;
        timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + ypos * ifTopPutPositive1, cam.transform.position.z + 17);
        timer += 1;
        if(timer > 20 && timer < 50)
        {
            ypos += -0.1f;
        }
        if(timer > 120 && timer< 140)
        {
            ypos += -0.2f;
        }
    }
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
