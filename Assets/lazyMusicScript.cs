using UnityEngine;

public class lazyMusicScript : MonoBehaviour
{
    public AudioSource audio;
    public GameObject rSelectMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rSelectMenu.active)
        {
            audio.enabled = true;
        }
        else
        {
            audio.enabled = false;
        }
    }
}
