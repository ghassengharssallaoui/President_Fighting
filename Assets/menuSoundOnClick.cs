using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSoundOnClick : MonoBehaviour
{
    BasicMenuOption bmo;
    int previousA;
    public GameObject sfxPrefab;
    public AudioClip audio;
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
            GameObject dummy;
            dummy = Instantiate(sfxPrefab);
            dummy.GetComponent<AudioSource>().clip = audio;
            dummy.GetComponent<AudioSource>().Play();
            previousA = bmo.aPress;
        }
    }
}
