using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadLevelOnEnable : MonoBehaviour
{
    public string level;

    // Start is called before the first frame update
    void OnEnable() //Should be on enable if I ever use this again.
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (level == "")
        {
            Application.LoadLevel("CharacterSelect");
        }
        else
        {
            Application.LoadLevel(level);

        }
    }
}
