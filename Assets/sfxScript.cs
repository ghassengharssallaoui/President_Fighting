using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxScript : MonoBehaviour
{
    public bool findplayer;
    public string soundID;
    public PlayerInfo info;
    public bool notSFX;
    public bool muted;
    public AudioSource audio;
    public BetterCameraMovement cam;
    public float loopTimeStamp;
    void volumeControl()
    {
        if (muted)
        {
            audio.volume = 0;
        }
        else
        {
            if (notSFX == false)
            {
                float dummyFloat = PlayerPrefs.GetInt("SFX Vol");
                audio.volume = dummyFloat / 100f;
            }
            else
            {
                float dummyFloat = PlayerPrefs.GetInt("Music Vol");
                audio.volume = dummyFloat / 100f;
                audio.loop = true;
                if (cam.p1 != null)
                {
                    if (cam.p1.GetComponent<PlayerInfo>().hit == 100)
                    {
                        audio.volume = dummyFloat / 300f;

                    }
                }
                if (cam.p2 != null)
                {
                    if (cam.p2.GetComponent<PlayerInfo>().hit == 100)
                    {
                        audio.volume = dummyFloat / 300f;

                    }
                }
                if (Time.timeScale < .25f && (cam.p2.GetComponent<PlayerInfo>().hit == 2 || cam.p1.GetComponent<PlayerInfo>().hit == 2))
                {
                    audio.volume = 0;
                }
            }
        }
    }
    void OnEnable()
    {
        cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        audio = GetComponent<AudioSource>();
        volumeControl();
    }
    void Start()
    {
        if (soundID == "Player")
        {
            if (findplayer)
            {
                info = GetComponent<findPlayer>().player;
            }
            if (info.currentAudio != null)
            {
                info.currentAudio.mute = true;
            }
            info.currentAudio = audio;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (audio.isPlaying && audio.volume != 0 && audio.mute != true)
        {
            bool alreadyInThere = false;

        }
        else
        {

        }

        volumeControl();
        if (soundID == "Player")
        {
            if (info.hit < 0)
            {
                audio.mute = true;
            }//we just letting this rock now
        }
        if (soundID != "Hit")
        {
            if (info != null)
            {
                if (info.hit == 1 || info.enemyScript.hit == 1)
                {
                    //audio.mute = true;
                    //this is just never happening for the time being
                }
            }
        }
        if (loopTimeStamp != 0)
        {
            if (audio.isPlaying == false)
            {
                audio.time = loopTimeStamp;
                audio.Play();
            }
        }
    }
    void OnDisable()
    {

    }
}
