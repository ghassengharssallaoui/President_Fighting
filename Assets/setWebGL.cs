using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setWebGL : MonoBehaviour
{
    public bool value;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject.Find("VersionDecider").GetComponent<VersionDecider>().WebGL = value;
    }
}
