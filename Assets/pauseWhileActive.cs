using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseWhileActive : MonoBehaviour
{
    // Start is called before the first frame update
    void OnDisable()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0.00001f;
    }
}
