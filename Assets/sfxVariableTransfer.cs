using UnityEngine;
using Unity.Netcode;

public class sfxVariableTransfer : NetworkBehaviour
{
    public NetworkVariable<float> volume = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> timeCode = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> mute = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> loop = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> soundID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public int numericID;
    int waitCounter;
    spriteGod god;
    void Start()
    {
        god = GameObject.Find("Sprite God").GetComponent<spriteGod>();
    }
    // Update is called once per frame
    void Update()
    {
        waitCounter += 1;
        if (waitCounter > 5)
        {
            if (IsServer)
            {
                if (god.AudioSourceList[numericID] != null)
                {
                    AudioSource myAudio = god.AudioSourceList[numericID];
                    volume.Value = myAudio.volume;
                    timeCode.Value = myAudio.time;
                    mute.Value = myAudio.mute;
                    loop.Value = myAudio.loop;
                    for (int i = 0; i < god.sfxList.Length; i++)
                    {
                        if (god.sfxList[i] == myAudio.clip)
                        {
                            soundID.Value = i;
                        }
                    }
                }
                else
                {
                    mute.Value = true;
                }
            }
            if (IsOwner)
            {
                AudioSource myAudio;
                myAudio = god.AudioSourcePainterList[numericID].GetComponent<AudioSource>();
                if (mute.Value != true)
                {
                    myAudio.mute = mute.Value;
                    if (volume.Value != 0)
                    {
                        float dummyFloat = PlayerPrefs.GetInt("SFX Vol");
                        myAudio.volume = dummyFloat / 100f;
                    }
                    myAudio.loop = loop.Value;
                    if (god.sfxList[soundID.Value] != myAudio.clip)
                    {
                        myAudio.clip = god.sfxList[soundID.Value];
                        myAudio.Play();
                    }
                    if(Mathf.Abs(myAudio.time - timeCode.Value) >= 1)
                    {
                        myAudio.time = timeCode.Value;
                        myAudio.Play();
                    }
                }
                else
                {
                    myAudio.mute = true;
                }
            }
        }
    }
}