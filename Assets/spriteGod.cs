using UnityEngine;

public class spriteGod : MonoBehaviour
{
    public Sprite[] spriteList;
    public AudioClip[] sfxList;
    public SpriteRenderer[] rendererList = new SpriteRenderer[100];
    public SpriteRenderer[] uiList = new SpriteRenderer[100];
    public SpriteRenderer[] painterList = new SpriteRenderer[100];
    public SpriteRenderer[] uipainterList = new SpriteRenderer[100];
    public networkTextVariables[] textPainterList = new networkTextVariables[100];
    public networkTextVariables[] textList = new networkTextVariables[100];
    public AudioSource[] AudioSourceList = new AudioSource[100];
    public AudioSource[] AudioSourcePainterList = new AudioSource[100];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
