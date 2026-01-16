using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnDestroy : MonoBehaviour
{
    public GameObject[] destroyObjs;
    void OnDestroy()
    {
        foreach(GameObject g in destroyObjs)
        {
            Destroy(g);
        }
    }

}
