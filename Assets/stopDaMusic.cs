using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopDaMusic : MonoBehaviour
{
    sfxScript audio;
    public bool musicOn;
    public bool onDisable;
    // Start is called before the first frame update
    void OnEnable()
    {
        audio = GameObject.Find("Music").GetComponent<sfxScript>();
        if(onDisable == false)
        {
            if (musicOn)
            {
                audio.muted = false;
            }
            else
            {
                audio.muted = true;
            }
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        if (onDisable == true)
        {
            if (musicOn)
            {
                audio.muted = false;
            }
            else
            {
                audio.muted = true;
            }
        }
    }
}
