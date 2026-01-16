using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseOnEnable : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable()
    {
        QualitySettings.vSyncCount = 1;
        QualitySettings.vSyncCount = 1;
        Time.timeScale = 0.00001f;
    }
}
