using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuControllerDisplayPrefText : MonoBehaviour
{
    public VerticalMenu menu;
    public string pref;
    int player;
    string keyboard;
    public int WebGLplayer;
    Text t;
    // Start is called before the first frame update
    void OnEnable()
    {
        player = menu.player;
        bool isKeyboard;
        isKeyboard = GameObject.Find("P" + player + "Input").GetComponent<InputGod>().isKeyboard;
        if (isKeyboard)
        {
            keyboard = "Keyboard";
        }
        else
        {
            keyboard = "Controller";
        }
        t = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string dummyString;
        if (WebGLplayer == 0)
        {
            dummyString = "P" + player + keyboard + pref;
        }
        else
        {
            dummyString = "P2" + keyboard + pref;
        }
        t.text = PlayerPrefs.GetString(dummyString);
    }
}
