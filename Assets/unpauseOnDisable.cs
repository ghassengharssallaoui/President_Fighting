using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unpauseOnDisable : MonoBehaviour
{
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
