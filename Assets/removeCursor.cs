using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        VersionDecider ver = GameObject.Find("VersionDecider").GetComponent<VersionDecider>();
        if (ver.WebGL == false && ver.camBuild == false)
        {
            Cursor.visible = false;
        }
    }
}
