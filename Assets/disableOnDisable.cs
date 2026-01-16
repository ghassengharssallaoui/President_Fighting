using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableOnDisable : MonoBehaviour
{
    public GameObject disable;
    void OnEnable()
    {
        if(disable == null)
        {
            disable = gameObject;
        }
    }
    // Start is called before the first frame update
    void OnDisable()
    {
        disable.active = false;
    }
}
