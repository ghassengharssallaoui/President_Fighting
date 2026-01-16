using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuChangeControllerPref : MonoBehaviour
{
    public VerticalMenu menu;
    public string pref;
    int player;
    string keyboard;
    public bool webGL;
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
        string dummyString;
        if (webGL == false)
        {
            dummyString = "ChangeP" + player + keyboard + pref;
        }
        else
        {
            dummyString = "ChangeP2" + keyboard + pref;
        }
        PlayerPrefs.SetInt(dummyString, 1);
    }
    void Update()
    {
        string dummyString;
        if (webGL == false)
        {
            dummyString = "ChangeP" + player + keyboard + pref;
        }
        else
        {
            dummyString = "ChangeP2" + keyboard + pref;
        }
    }
    void OnDisable()
    {
        string dummyString;
        if (webGL == false)
        {
            dummyString = "ChangeP" + player + keyboard + pref;
        }
        else
        {
            dummyString = "ChangeP2" + keyboard + pref;
        }
        PlayerPrefs.SetInt(dummyString, 0);
    }

    // Update is called once per frame

}
