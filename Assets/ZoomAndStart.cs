using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomAndStart : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        timer = 0;
        transform.position = new Vector3(0, 5, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= 0)
        {
            transform.position = new Vector3(0, 5, 0);
            timer += (Time.deltaTime / Time.timeScale);
            if (timer > 0.5f)
            {
                gameObject.active = false;
            }
        }
        else
        {
            if (Time.deltaTime < 0.001f)
            {
                transform.position += new Vector3(0, 0, -150 * (Time.deltaTime / Time.timeScale));

            }
            Time.timeScale = 0.00001f;
        }
        
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
