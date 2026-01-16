using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeVersion : MonoBehaviour
{
    BasicMenuOption bmo;
    int previousA;
    // Start is called before the first frame update
    void OnEnable()
    {
        bmo = GetComponent<BasicMenuOption>();
        previousA = bmo.aPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (bmo.aPress > previousA)
        {
            GameObject.Find("VersionDecider").GetComponent<VersionDecider>().WebGL = true;
            Application.LoadLevel("CharacterSelect");
        }
    }
}
