using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class superMeterGod : MonoBehaviour
{
    bool initialize;
    public int player;
    public PlayerInfo info;
    public SpriteRenderer scoreSprite;
    public Sprite[] scoreSpriteAssets;
    void OnEnable()
    {
        initialize = true;
    }
    void Initialize()
    {
        initialize = false;
        BetterCameraMovement cam;
        cam = GameObject.Find("Main Camera").GetComponent<BetterCameraMovement>();
        if(player == 1)
        {
            info = cam.p1.GetComponent<PlayerInfo>();
        }
        else
        {
            info = cam.p2.GetComponent<PlayerInfo>();
        }
    }
    void UpdateAlways() //I don't know why I'm doing it like this
    {
        scoreSprite.sprite = scoreSpriteAssets[info.superCost];
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (initialize)
        {
            Initialize();
        }
        UpdateAlways();
    }
}
