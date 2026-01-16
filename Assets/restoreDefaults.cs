using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restoreDefaults : MonoBehaviour
{
    BasicMenuOption bmo;
    public VerticalMenu menu;
    public int previousA;
    public bool WebGL;
    public bool doSoon;
    public ReceiveInputs receiver;
    // Start is called before the first frame update
    void OnEnable()
    {
        bmo = GetComponent<BasicMenuOption>();
        previousA = bmo.aPress;
        doSoon = false;
        if(menu.player == 1)
        {
            receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        }
        else
        {
            receiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();

        }
    }
    void Reset()
    {
        print("resetting");
        bool isKeyboard;
        isKeyboard = GameObject.Find("P" + menu.player + "Input").GetComponent<InputGod>().isKeyboard;
        if (menu.player == 1 && isKeyboard)
        {
            PlayerPrefs.SetString("P1KeyboardMoveScheme", "WASD");
            PlayerPrefs.SetString("P1KeyboardA", "J");
            PlayerPrefs.SetString("P1KeyboardB", "K");
            PlayerPrefs.SetString("P1KeyboardC", "L");
            PlayerPrefs.SetString("P1KeyboardD", "I");
            PlayerPrefs.SetString("P1KeyboardJ", "Space");
            PlayerPrefs.SetString("P1KeyboardTapJump", "On");
        }
        if (menu.player == 2 && isKeyboard)
        {
            PlayerPrefs.SetString("P2KeyboardMoveScheme", "WASD");
            PlayerPrefs.SetString("P2KeyboardA", "J");
            PlayerPrefs.SetString("P2KeyboardB", "K");
            PlayerPrefs.SetString("P2KeyboardC", "L");
            PlayerPrefs.SetString("P2KeyboardD", "I");
            PlayerPrefs.SetString("P2KeyboardJ", "Space");
            PlayerPrefs.SetString("P2KeyboardTapJump", "On");
        }
        if (menu.player == 1 && isKeyboard == false)
        {
            PlayerPrefs.SetString("P1ControllerMoveScheme", "L Stick");
            PlayerPrefs.SetString("P1ControllerA", "Bttn S");
            PlayerPrefs.SetString("P1ControllerB", "Bttn W");
            PlayerPrefs.SetString("P1ControllerC", "R2");
            PlayerPrefs.SetString("P1ControllerD", "Bttn E");
            PlayerPrefs.SetString("P1ControllerJ", "Bttn N");
            PlayerPrefs.SetString("P1ControllerTapJump", "Off");
        }
        if (menu.player == 2 && isKeyboard == false)
        {
            PlayerPrefs.SetString("P2ControllerMoveScheme", "L Stick");
            PlayerPrefs.SetString("P2ControllerA", "Bttn S");
            PlayerPrefs.SetString("P2ControllerB", "Bttn W");
            PlayerPrefs.SetString("P2ControllerC", "R2");
            PlayerPrefs.SetString("P2ControllerD", "Bttn E");
            PlayerPrefs.SetString("P2ControllerJ", "Bttn N");
            PlayerPrefs.SetString("P2ControllerTapJump", "Off");
        }
        if (WebGL)
        {
            PlayerPrefs.SetString("P2KeyboardMoveScheme", "Arrow Keys");
            PlayerPrefs.SetString("P2KeyboardA", "p");
            PlayerPrefs.SetString("P2KeyboardB", "[");
            PlayerPrefs.SetString("P2KeyboardC", "]");
            PlayerPrefs.SetString("P2KeyboardD", "-");
            PlayerPrefs.SetString("P2KeyboardJ", "bck slash");
            PlayerPrefs.SetString("P2KeyboardTapJump", "On");
        }
        receiver.attack = false;
        receiver.holdingAttack = 0;
        receiver.special = false;
        receiver.holdingSpecial = 0;
        receiver.movement = false;
        receiver.holdingMovement = 0;
        receiver.super = false;
        receiver.holdingSuper = 0;
        receiver.jump = false;
        receiver.holdingJump = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (bmo.aPress > previousA)
        {
            print("dosoon");
            previousA = bmo.aPress;
            doSoon = true;
        }
        if (doSoon)
        {
            print("dosoon2");
            if (receiver.attack == false)
            {
                Reset();
                doSoon = false;
            }
        }
    }
}
