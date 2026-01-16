using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createPlayerPrefs : MonoBehaviour
{
    public bool godDecider; //this prevents any other instances of the script that might be fucking me up from running;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        if (godDecider)
        {
            if (GameObject.Find("VersionDecider") != null)
            {
                if (GameObject.Find("VersionDecider").GetComponent<VersionDecider>().WebGL)
                {
                    PlayerPrefs.DeleteAll();
                }
            }
            if (PlayerPrefs.GetInt("SFX Vol", 30) == 30)
            {
                PlayerPrefs.SetInt("SFX Vol", 30);
            }
            if (PlayerPrefs.GetInt("Music Vol", 30) == 30)
            {
                PlayerPrefs.SetInt("Music Vol", 30);
            }
        }
    }
}
