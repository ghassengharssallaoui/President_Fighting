using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathSlowMo : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Time.timeScale = 0.25f;
    }

    // Update is called once per frame
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
