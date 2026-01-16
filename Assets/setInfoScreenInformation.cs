using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setInfoScreenInformation : MonoBehaviour
{
    public charInfo2 Overview;
    public GameObject OverviewHOVER;
    public charInfo2 Move1;
    public GameObject Move1HOVER;
    public charInfo2 Move2;
    public GameObject Move2HOVER;
    public charInfo2 Move3;
    public GameObject Move3HOVER;
    public charInfo2 Move4;
    public GameObject Move4HOVER;
    public charInfo2 Move5;
    public GameObject Move5HOVER;
    public charInfo2 Move6;
    public GameObject Move6HOVER;
    public charInfo2 Move7;
    public GameObject Move7HOVER;
    public charInfo2 Move8;
    public GameObject Move8HOVER;
    public charInfo2 Move9;
    public GameObject Move9HOVER;
    public charInfo2 Passive1;
    public GameObject Passive1HOVER;
    public charInfo2 Passive2;
    public GameObject Passive2HOVER;
    public charInfo2 Super1;
    public GameObject Super1HOVER;
    public charInfo2 Super2;
    public GameObject Super2HOVER;
    public charInfo2 Super3;
    public GameObject Super3HOVER;
    public charInfo2 Super4;
    public GameObject Super4HOVER;
    public charInfo2 Super5;
    public GameObject Super5HOVER;
    public GameObject Back5HOVER;
    public BetterCameraMovement cam;
    int iterator;
    CharacterInfo info;
    Vector3 startingHeaderLocation;
    VerticalMenu menu;
    // Start is called before the first frame update
    void OnEnable()
    {
        startingHeaderLocation = Overview.headerSprite.transform.position;
        iterator = 0;
        int playerint = GetComponent<VerticalMenu>().player;
        menu = GetComponent<VerticalMenu>();
        if (cam.p1 != null)
        {
            if (playerint == 1)
            {
                info = cam.p1.GetComponent<CharacterInfo>();
            }
            else
            {
                info = cam.p2.GetComponent<CharacterInfo>();
            }
            for (int i = 0; i < menu.menuOptions.Length; i++)
            {
                menu.menuOptions[i] = Back5HOVER;
            }
            updateInfo(info.Overview, Overview, OverviewHOVER);
            updateInfo(info.Move1, Move1, Move1HOVER);
            updateInfo(info.Move2, Move2, Move2HOVER);
            updateInfo(info.Move3, Move3, Move3HOVER);
            updateInfo(info.Move4, Move4, Move4HOVER);
            updateInfo(info.Move5, Move5, Move5HOVER);
            updateInfo(info.Move6, Move6, Move6HOVER);
            updateInfo(info.Move7, Move7, Move7HOVER);
            updateInfo(info.Move8, Move8, Move8HOVER);
            updateInfo(info.Move9, Move9, Move9HOVER);
            updateInfo(info.Passive1, Passive1, Passive1HOVER);
            updateInfo(info.Passive2, Passive2, Passive2HOVER);
            updateInfo(info.Super1, Super1, Super1HOVER);
            updateInfo(info.Super2, Super2, Super2HOVER);
            updateInfo(info.Super3, Super3, Super3HOVER);
            updateInfo(info.Super4, Super4, Super4HOVER);
            updateInfo(info.Super5, Super5, Super5HOVER);

        }
    }
    void updateInfo(charInfo info1, charInfo2 info2, GameObject hoverObj)
    {
        if (info2.input1 != null)
        {
            info2.input1.sprite = null;
        }
        if (info2.input2 != null)
        {
            info2.input2.sprite = null;
        }
        if (info2.input3 != null)
        {
            info2.input3.sprite = null;
        }
        if (info2.input4 != null)
        {
            info2.input4.sprite = null;
        }
        //hoverObj.active = false;
        if (info1.title == null || info1.title == "")
        {
            info2.title.text = "";
            info2.description.text = null;
            info2.headerSprite.sprite = null;
        }
        else
        {
            info2.title.text = info1.title;
            info2.description.text = info1.description;
            info2.headerSprite.sprite = info1.headerSprite;
            info2.headerSprite.transform.localScale = new Vector3(info1.headerSize * 0.05f, info1.headerSize, info1.headerSize);
            info2.headerSprite.transform.position = startingHeaderLocation + info1.headerDisplacement;
            if (info1.input1 != null)
            {
                info2.input1.sprite = info1.input1;
            }
            if (info1.input2 != null)
            {
                info2.input2.sprite = info1.input2;
            }
            if (info1.input3 != null)
            {
                info2.input3.sprite = info1.input3;
            }
            if (info1.input4 != null)
            {
                info2.input4.sprite = info1.input4;
            }
            menu.menuOptions[iterator] = hoverObj;
            iterator += 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
