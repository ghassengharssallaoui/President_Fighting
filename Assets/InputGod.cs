using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputGod : MonoBehaviour
{
    /// <summary>
    /// So here's how I anticipate this working: I will have OnCONTROLLER option for all options
    /// then I will have them run a function like checkMapping(string). That string will be checked against eight player prefs
    /// When we set new controls. We'll put them on player prefs.
    /// We'll have an additional pref for controller type. This will determine default mapping, and if we reset to default upon connecting
    /// 
    /// Upon spawning, we'll find out what kind of controller
    /// need to add no pause failsafe on menus
    /// hey, while we're using this as a to do list, we need to separate the sfx prefab on supers or merge them in audacity
    /// Need to add both players on one keyboard for webgl version
    /// ^Think I did that, but we'll need a special controlller input screen for webgl keyboard. Move scheme can't change, and player 1 and 2 buttons can change at the same time
    /// Up
    /// 
    /// Up next: Can we get the buttons to display on the menu and can we change them by setting xxxxxchangexxxx to true and false when hovering
    /// 
    /// </summary>
    ReceiveInputs receiver;
    ReceiveInputs doubleReceiver; //to be used with WEBGL single keyboard.
    int player;
    public bool WebGl;
    public bool mobile;
    public bool playerSet;
    public bool isKeyboard;
    public bool webGLDone;
    public bool onlineDummys;
    void SetWebGLDefaults()
    {
            PlayerPrefs.SetString("P2KeyboardMoveScheme", "Arrow Keys");
            PlayerPrefs.SetString("P2KeyboardA", "P");
            PlayerPrefs.SetString("P2KeyboardB", "[");
            PlayerPrefs.SetString("P2KeyboardC", "]");
            PlayerPrefs.SetString("P2KeyboardD", "-");
            PlayerPrefs.SetString("P2KeyboardJ", "=");
            PlayerPrefs.SetString("P2KeyboardTapJump", "On");
        webGLDone = true;
    }
    void SetDefaults()
    {
        if (GameObject.Find("VersionDecider") == null)
        {
            WebGl = false;
        }
        else
        {
            WebGl = GameObject.Find("VersionDecider").GetComponent<VersionDecider>().WebGL;
            if (WebGl && webGLDone == false)
            {
                SetWebGLDefaults();
                
            }
        }
        if (PlayerPrefs.GetString("P1KeyboardMoveScheme") == "")
        {
            PlayerPrefs.SetString("P1KeyboardMoveScheme", "WASD");
        }
        if (PlayerPrefs.GetString("P1KeyboardA") == "")
        {
            PlayerPrefs.SetString("P1KeyboardA", "J");
        }
        if (PlayerPrefs.GetString("P1KeyboardB") == "")
        {
            PlayerPrefs.SetString("P1KeyboardB", "K");
        }
        if (PlayerPrefs.GetString("P1KeyboardC") == "")
        {
            PlayerPrefs.SetString("P1KeyboardC", "L");
        }
        if (PlayerPrefs.GetString("P1KeyboardD") == "")
        {
            PlayerPrefs.SetString("P1KeyboardD", "I");
        }
        if (PlayerPrefs.GetString("P1KeyboardJ") == "")
        {
            PlayerPrefs.SetString("P1KeyboardJ", "Space");
        }
        if (PlayerPrefs.GetString("P1KeyboardTapJump") == "")
        {
            PlayerPrefs.SetString("P1KeyboardTapJump", "On");
        }
        ///
        ///
        if (PlayerPrefs.GetString("P2KeyboardMoveScheme") == "")
        {
            PlayerPrefs.SetString("P2KeyboardMoveScheme", "WASD");
        }
        if (PlayerPrefs.GetString("P2KeyboardA") == "")
        {
            PlayerPrefs.SetString("P2KeyboardA", "J");
        }
        if (PlayerPrefs.GetString("P2KeyboardB") == "")
        {
            PlayerPrefs.SetString("P2KeyboardB", "K");
        }
        if (PlayerPrefs.GetString("P2KeyboardC") == "")
        {
            PlayerPrefs.SetString("P2KeyboardC", "L");
        }
        if (PlayerPrefs.GetString("P2KeyboardD") == "")
        {
            PlayerPrefs.SetString("P2KeyboardD", "I");
        }
        if (PlayerPrefs.GetString("P2KeyboardJ") == "")
        {
            PlayerPrefs.SetString("P2KeyboardJ", "Space");
        }
        if (PlayerPrefs.GetString("P2KeyboardTapJump") == "")
        {
            PlayerPrefs.SetString("P2KeyboardTapJump", "On");
        }
        ///
        ///
        if (PlayerPrefs.GetString("P1ControllerMoveScheme") == "")
        {
            PlayerPrefs.SetString("P1ControllerMoveScheme", "L Stick");
        }
        if (PlayerPrefs.GetString("P1ControllerA") == "")
        {
            PlayerPrefs.SetString("P1ControllerA", "Bttn S");
        }
        if (PlayerPrefs.GetString("P1ControllerB") == "")
        {
            PlayerPrefs.SetString("P1ControllerB", "Bttn W");
        }
        if (PlayerPrefs.GetString("P1ControllerC") == "")
        {
            PlayerPrefs.SetString("P1ControllerC", "R2");
        }
        if (PlayerPrefs.GetString("P1ControllerD") == "")
        {
            PlayerPrefs.SetString("P1ControllerD", "Bttn E");
        }
        if (PlayerPrefs.GetString("P1ControllerJ") == "")
        {
            PlayerPrefs.SetString("P1ControllerJ", "Bttn N");
        }
        if (PlayerPrefs.GetString("P1ControllerTapJump") == "")
        {
            PlayerPrefs.SetString("P1ControllerTapJump", "Off");
        }
        ///
        ///
        if (PlayerPrefs.GetString("P2ControllerMoveScheme") == "")
        {
            PlayerPrefs.SetString("P2ControllerMoveScheme", "L Stick");
        }
        if (PlayerPrefs.GetString("P2ControllerA") == "")
        {
            PlayerPrefs.SetString("P2ControllerA", "Bttn S");
        }
        if (PlayerPrefs.GetString("P2ControllerB") == "")
        {
            PlayerPrefs.SetString("P2ControllerB", "Bttn W");
        }
        if (PlayerPrefs.GetString("P2ControllerC") == "")
        {
            PlayerPrefs.SetString("P2ControllerC", "R2");
        }
        if (PlayerPrefs.GetString("P2ControllerD") == "")
        {
            PlayerPrefs.SetString("P2ControllerD", "Bttn E");
        }
        if (PlayerPrefs.GetString("P2ControllerJ") == "")
        {
            PlayerPrefs.SetString("P2ControllerJ", "Bttn N");
        }
        if (PlayerPrefs.GetString("P2ControllerTapJump") == "")
        {
            PlayerPrefs.SetString("P2ControllerTapJump", "Off");
        }
        ///
        ///
    }

    void Player1KeyboardInterprets(string s)
    {
        //next step is to turn on tap jump
        if (PlayerPrefs.GetInt("ChangeP1KeyboardA") == 1)
        {
            string olds = PlayerPrefs.GetString("P1KeyboardA");
            PlayerPrefs.SetString("P1KeyboardA", s);
            if (PlayerPrefs.GetString("P1KeyboardB") == s)
            {
                PlayerPrefs.SetString("P1KeyboardB", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP1KeyboardB") == 1)
        {
            string olds = PlayerPrefs.GetString("P1KeyboardB");
            PlayerPrefs.SetString("P1KeyboardB", s);
            if (PlayerPrefs.GetString("P1KeyboardA") == s)
            {
                PlayerPrefs.SetString("P1KeyboardA", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP1KeyboardC") == 1)
        {
            PlayerPrefs.SetString("P1KeyboardC", s);
        }
        if (PlayerPrefs.GetInt("ChangeP1KeyboardD") == 1)
        {
            PlayerPrefs.SetString("P1KeyboardD", s);
        }
        if (PlayerPrefs.GetInt("ChangeP1KeyboardJ") == 1)
        {
            PlayerPrefs.SetString("P1KeyboardJ", s);
        }
        if (s == PlayerPrefs.GetString("P1KeyboardA"))
        {
            OnAttack();
        }
        if (s == PlayerPrefs.GetString("P1KeyboardB"))
        {
            OnSpecial();
        }
        if (s == PlayerPrefs.GetString("P1KeyboardC"))
        {
            OnMovement();
        }
        if (s == PlayerPrefs.GetString("P1KeyboardD"))
        {
            OnSuper();
        }
        if (s == PlayerPrefs.GetString("P1KeyboardJ"))
        {
            OnJump();
        }
        if (WebGl)
        {
            if (PlayerPrefs.GetInt("ChangeP2KeyboardA") == 1)
            {
                string olds = PlayerPrefs.GetString("P2KeyboardA");
                PlayerPrefs.SetString("P2KeyboardA", s);
                if (PlayerPrefs.GetString("P2KeyboardB") == s)
                {
                    PlayerPrefs.SetString("P2KeyboardB", olds);
                }
            }
            if (PlayerPrefs.GetInt("ChangeP2KeyboardB") == 1)
            {
                string olds = PlayerPrefs.GetString("P2KeyboardB");
                PlayerPrefs.SetString("P2KeyboardB", s);
                if (PlayerPrefs.GetString("P2KeyboardA") == s)
                {
                    PlayerPrefs.SetString("P2KeyboardA", olds);
                }
            }
            if (PlayerPrefs.GetInt("ChangeP2KeyboardC") == 1)
            {
                PlayerPrefs.SetString("P2KeyboardC", s);
            }
            if (PlayerPrefs.GetInt("ChangeP2KeyboardD") == 1)
            {
                PlayerPrefs.SetString("P2KeyboardD", s);
            }
            if (PlayerPrefs.GetInt("ChangeP2KeyboardJ") == 1)
            {
                PlayerPrefs.SetString("P2KeyboardJ", s);
            }
            if (s == PlayerPrefs.GetString("P2KeyboardA"))
            {
                OnWebGLAttack();
            }
            if (s == PlayerPrefs.GetString("P2KeyboardB"))
            {
                OnWebGLSpecial();
            }
            if (s == PlayerPrefs.GetString("P2KeyboardC"))
            {
                OnWebGLMovement();
            }
            if (s == PlayerPrefs.GetString("P2KeyboardD"))
            {
                OnWebGLSuper();
            }
            if (s == PlayerPrefs.GetString("P2KeyboardJ"))
            {
                OnWebGLJump();
            }
        }
    }
    void Player2KeyboardInterprets(string s)
    {
        if (PlayerPrefs.GetInt("ChangeP2KeyboardA") == 1)
        {
            string olds = PlayerPrefs.GetString("P2KeyboardA");
            PlayerPrefs.SetString("P2KeyboardA", s);
            if (PlayerPrefs.GetString("P2KeyboardB") == s)
            {
                PlayerPrefs.SetString("P2KeyboardB", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP2KeyboardB") == 1)
        {
            string olds = PlayerPrefs.GetString("P2KeyboardB");
            PlayerPrefs.SetString("P2KeyboardB", s);
            if (PlayerPrefs.GetString("P2KeyboardA") == s)
            {
                PlayerPrefs.SetString("P2KeyboardA", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP2KeyboardC") == 1)
        {
            PlayerPrefs.SetString("P2KeyboardC", s);
        }
        if (PlayerPrefs.GetInt("ChangeP2KeyboardD") == 1)
        {
            PlayerPrefs.SetString("P2KeyboardD", s);
        }
        if (PlayerPrefs.GetInt("ChangeP2KeyboardJ") == 1)
        {
            PlayerPrefs.SetString("P2KeyboardJ", s);
        }
        if (s == PlayerPrefs.GetString("P2KeyboardA"))
        {
            OnAttack();
        }
        if (s == PlayerPrefs.GetString("P2KeyboardB"))
        {
            OnSpecial();
        }
        if (s == PlayerPrefs.GetString("P2KeyboardC"))
        {
            OnMovement();
        }
        if (s == PlayerPrefs.GetString("P2KeyboardD"))
        {
            OnSuper();
        }
        if (s == PlayerPrefs.GetString("P2KeyboardJ"))
        {
            OnJump();
        }
    }
    void Player1ControllerInterprets(string s)
    {
        if (PlayerPrefs.GetInt("ChangeP1ControllerA") == 1)
        {
            string olds = PlayerPrefs.GetString("P1ControllerA");
            PlayerPrefs.SetString("P1ControllerA", s);
            if (PlayerPrefs.GetString("P1ControllerB") == s)
            {
                PlayerPrefs.SetString("P1ControllerB", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP1ControllerB") == 1)
        {
            string olds = PlayerPrefs.GetString("P1ControllerB");
            PlayerPrefs.SetString("P1ControllerB", s);
            if (PlayerPrefs.GetString("P1ControllerA") == s)
            {
                PlayerPrefs.SetString("P1ControllerA", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP1ControllerC") == 1)
        {
            PlayerPrefs.SetString("P1ControllerC", s);
        }
        if (PlayerPrefs.GetInt("ChangeP1ControllerD") == 1)
        {
            PlayerPrefs.SetString("P1ControllerD", s);
        }
        if (PlayerPrefs.GetInt("ChangeP1ControllerJ") == 1)
        {
            PlayerPrefs.SetString("P1ControllerJ", s);
        }
        if (s == PlayerPrefs.GetString("P1ControllerA"))
        {
            OnAttack();
        }
        if (s == PlayerPrefs.GetString("P1ControllerB"))
        {
            OnSpecial();
        }
        if (s == PlayerPrefs.GetString("P1ControllerC"))
        {
            OnMovement();
        }
        if (s == PlayerPrefs.GetString("P1ControllerD"))
        {
            OnSuper();
        }
        if (s == PlayerPrefs.GetString("P1ControllerJ"))
        {
            OnJump();
        }
    }
    void Player2ControllerInterprets(string s)
    {
        if (PlayerPrefs.GetInt("ChangeP2ControllerA") == 1)
        {
            string olds = PlayerPrefs.GetString("P2ControllerA");
            PlayerPrefs.SetString("P2ControllerA", s);
            if (PlayerPrefs.GetString("P2ControllerB") == s)
            {
                PlayerPrefs.SetString("P2ControllerB", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP2ControllerB") == 1)
        {
            string olds = PlayerPrefs.GetString("P2ControllerB");
            PlayerPrefs.SetString("P2ControllerB", s);
            if (PlayerPrefs.GetString("P2ControllerA") == s)
            {
                PlayerPrefs.SetString("P2ControllerA", olds);
            }
        }
        if (PlayerPrefs.GetInt("ChangeP2ControllerC") == 1)
        {
            PlayerPrefs.SetString("P2ControllerC", s);
        }
        if (PlayerPrefs.GetInt("ChangeP2ControllerD") == 1)
        {
            PlayerPrefs.SetString("P2ControllerD", s);
        }
        if (PlayerPrefs.GetInt("ChangeP2ControllerJ") == 1)
        {
            PlayerPrefs.SetString("P2ControllerJ", s);
        }
        if (s == PlayerPrefs.GetString("P2ControllerA"))
        {
            OnAttack();
        }
        if (s == PlayerPrefs.GetString("P2ControllerB"))
        {
            OnSpecial();
        }
        if (s == PlayerPrefs.GetString("P2ControllerC"))
        {
            OnMovement();
        }
        if (s == PlayerPrefs.GetString("P2ControllerD"))
        {
            OnSuper();
        }
        if (s == PlayerPrefs.GetString("P2ControllerJ"))
        {
            OnJump();
        }
    }
    void InterpretInput(string s)
    {
        if (player == 1 && isKeyboard)
        {
            Player1KeyboardInterprets(s);
        }
        if ((player == 2 && isKeyboard ))
        {
            Player2KeyboardInterprets(s);
        }
        if (player == 1 && isKeyboard == false)
        {
            Player1ControllerInterprets(s);
        }
        if (player == 2 && isKeyboard == false)
        {
            Player2ControllerInterprets(s);
        }
        //ifWebgl: Do doublekeyboard interprets
    }
    void SetPlayer()
    {
        if (GameObject.Find("P1Input") == null)
        {
            gameObject.name = "P1Input";
            receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
            doubleReceiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
            player = 1;

        }
        else if (GameObject.Find("P2Input") == null)
        {
            gameObject.name = "P2Input";
            receiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
            player = 2;
        }
        else
        {
            gameObject.name = "MiscInput";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        receiver = GameObject.Find("DummyReceiver").GetComponent<ReceiveInputs>();
        int i = 0;
        GameObject spawner = GameObject.Find("InputSpawner");
        while (spawner.GetComponent<inputHolderForDestroy>().inputs[i] != null)
        {
            i++;
        }
        spawner.GetComponent<inputHolderForDestroy>().inputs[i] = gameObject;
        //some logic later
        SetDefaults();
        mobile = GameObject.Find("VersionDecider").GetComponent<VersionDecider>().mobile;
        if (mobile)
        {
            OnConnectController();
        }
        if (onlineDummys)
        {
            GetComponent<InputGod>().enabled = false;
        }
    }
    void OnConnectKeyboard()
    {
        if (playerSet == false)
        {
            if (GameObject.Find("P1Input") == null)
            {
                gameObject.name = "P1Input";
                receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
                receiver.myInput = GetComponent<InputGod>();
                player = 1;
                doubleReceiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
                if (WebGl)
                {
                    doubleReceiver.myInput = GetComponent<InputGod>();
                    doubleReceiver.tapJump = true;
                }

            }
            else if (GameObject.Find("P2Input") == null)
            {
                gameObject.name = "P2Input";
                receiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
                receiver.myInput = GetComponent<InputGod>();

                player = 2;
            }
            else
            {
                gameObject.name = "MiscInput";
            }
            if (PlayerPrefs.GetString("P1KeyboardTapJump") == "")
            {
                PlayerPrefs.SetString("P1KeyboardTapJump", "On");
            }
            isKeyboard = true;
        }
        playerSet = true;
        SetDefaults();
    }
    void OnConnectController()
    {
        if (playerSet == false)
        {
            if (GameObject.Find("P1Input") == null)
            {
                gameObject.name = "P1Input";
                receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
                player = 1;
                receiver.myInput = GetComponent<InputGod>();

            }
            else if (GameObject.Find("P2Input") == null)
            {
                gameObject.name = "P2Input";
                receiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
                receiver.myInput = GetComponent<InputGod>();

                player = 2;
            }
            else
            {
                gameObject.name = "MiscInput";
            }
            isKeyboard = false;
        }
        playerSet = true;
        SetDefaults();

    }
    void OnArrowKeysMove(InputValue value)
    {
        if ((player == 1 && PlayerPrefs.GetString("P1KeyboardMoveScheme") == "Arrow Keys")) {
            receiver.moveVector = value.Get<Vector2>();
        }
        if ((player == 2 && PlayerPrefs.GetString("P2KeyboardMoveScheme") == "Arrow Keys")) {
            receiver.moveVector = value.Get<Vector2>();
        }
        if (WebGl)
        {
            doubleReceiver.moveVector = value.Get<Vector2>();
        }
    }
    void OnWASDMove(InputValue value)
    {
        if ((player == 1 && PlayerPrefs.GetString("P1KeyboardMoveScheme") == "WASD")) {
            receiver.moveVector = value.Get<Vector2>();
        }
        if ((player == 2 && PlayerPrefs.GetString("P2KeyboardMoveScheme") == "WASD")) {
            receiver.moveVector = value.Get<Vector2>();
        }
    }
    void OnLStick(InputValue value)
    {
        if ((player == 1 && PlayerPrefs.GetString("P1ControllerMoveScheme") == "L Stick"))
        {
            receiver.moveVector = value.Get<Vector2>();
        }
        if ((player == 2 && PlayerPrefs.GetString("P2ControllerMoveScheme") == "L Stick"))
        {
            receiver.moveVector = value.Get<Vector2>();
        }
    }
    void OnRStick(InputValue value)
    {
        if ((player == 1 && PlayerPrefs.GetString("P1ControllerMoveScheme") == "R Stick"))
        {
            receiver.moveVector = value.Get<Vector2>();
        }
        if ((player == 2 && PlayerPrefs.GetString("P2ControllerMoveScheme") == "R Stick"))
        {
            receiver.moveVector = value.Get<Vector2>();
        }
    }
    void OnDPad(InputValue value)
    {
        if ((player == 1 && PlayerPrefs.GetString("P1ControllerMoveScheme") == "D-Pad"))
        {
            receiver.moveVector = value.Get<Vector2>();
        }
        if ((player == 2 && PlayerPrefs.GetString("P2ControllerMoveScheme") == "D-Pad"))
        {
            receiver.moveVector = value.Get<Vector2>();
        }
    }
    void OnStart()
    {
        if (receiver.start)
        {
            receiver.start = false;
        }
        else
        {
            receiver.start = true;
            receiver.holdingStart = 1;
        }
    }
    void CycleTapJump()
    {
        string keyboard;
        if (isKeyboard)
        {
            keyboard = "Keyboard";
        }
        else
        {
            keyboard = "Controller";
        }
        string pref;
        pref = "P" + player + keyboard + "TapJump";
        if (PlayerPrefs.GetInt("Change" + pref) == 1)
        {
            if (PlayerPrefs.GetString(pref) == "Off")
            {
                PlayerPrefs.SetString(pref, "On");
            }
            else
            {
                PlayerPrefs.SetString(pref, "Off");
            }
        }
    }
    void CycleMovement()
    {
        string dummyString;
        dummyString = "ChangeP" + player;
        if (isKeyboard)
        {
            dummyString += "Keyboard";
        }
        else
        {
            dummyString += "Controller";
        }
        dummyString += "MoveScheme";
        if (PlayerPrefs.GetInt(dummyString) == 1)
        {
            if (isKeyboard)
            {
                string dummyString2 = PlayerPrefs.GetString("P"+player+"KeyboardMoveScheme");
                if (dummyString2 == "WASD")
                {
                    PlayerPrefs.SetString("P" + player + "KeyboardMoveScheme", "Arrow Keys");
                }
                else
                {
                    PlayerPrefs.SetString("P" + player + "KeyboardMoveScheme", "WASD");
                }
                //PlayerPrefs.SetString(dummyString, dummyString2);
            }
            else
            {
                string dummyString2 = PlayerPrefs.GetString("P" + player + "ControllerMoveScheme");
                if (dummyString2 == "L Stick")
                {
                    PlayerPrefs.SetString("P" + player + "ControllerMoveScheme", "R Stick");
                }
                else if(dummyString2 == "R Stick")
                {
                    PlayerPrefs.SetString("P" + player + "ControllerMoveScheme", "D-Pad");
                }
                else
                {
                    PlayerPrefs.SetString("P" + player + "ControllerMoveScheme", "L Stick");
                }
            }
        }
    }
    void Update() //setting tap jump here because I want to.
    {
        if (player == 1)
        {
            if (isKeyboard)
            {
                if (PlayerPrefs.GetString("P1KeyboardTapJump") == "On")
                {
                    receiver.tapJump = true;
                }
                else
                {
                    receiver.tapJump = false;
                }
            }
            else
            {
                if (PlayerPrefs.GetString("P1ControllerTapJump") == "On" && mobile == false)
                {
                    receiver.tapJump = true;
                }
                else
                {
                    receiver.tapJump = false;
                }
            }
        }
        else
        {
            if (isKeyboard)
            {
                if (PlayerPrefs.GetString("P2KeyboardTapJump") == "On")
                {
                    receiver.tapJump = true;
                }
                else
                {
                    receiver.tapJump = false;
                }
            }
            else
            {
                if (PlayerPrefs.GetString("P2ControllerTapJump") == "On")
                {
                    receiver.tapJump = true;
                }
                else
                {
                    receiver.tapJump = false;
                }
            }
        }
    }
    void OnJ()
    {
        InterpretInput("J");
    }
    void OnK()
    {
        InterpretInput("K");
    }
    void OnL()
    {
        InterpretInput("L");
    }
    void OnI()
    {
        InterpretInput("I");
    }
    void OnSpace()
    {
        InterpretInput("Space");
    }
    /// <summary>
    /// CONTROLLERS//////////////////////////
    /// </summary>
    /// 
    void OnBttnN()
    {
        InterpretInput("Bttn N");
    }
    void OnBttnW()
    {
        InterpretInput("Bttn W");
    }
    void OnBttnE()
    {
        InterpretInput("Bttn E");
    }
    void OnBttnS()
    {
        InterpretInput("Bttn S");
    }
    void OnR1()
    {
        InterpretInput("R1");
    }
    void OnR2()
    {
        InterpretInput("R2");
    }
    void OnL1()
    {
        InterpretInput("L1");
    }
    void OnL2()
    {
        InterpretInput("L2");
    }
    /// <summary>
    /// MASSIVE Keyboard incoming
    /// </summary>
    void OnBackTick()
    {
        InterpretInput("`");
    }
    void On_1()
    {
        InterpretInput("1");
    }
    void On_2()
    {
        InterpretInput("2");
    }
    void On_3()
    {
        InterpretInput("3");
    }
    void On_4()
    {
        InterpretInput("4");
    }
    void On_5()
    {
        InterpretInput("5");
    }
    void On_6()
    {
        InterpretInput("6");
    }
    void On_7()
    {
        InterpretInput("7");
    }
    void On_8()
    {
        InterpretInput("8");
    }
    void On_9()
    {
        InterpretInput("9");
    }
    void On_0()
    {
        InterpretInput("0");
    }
    void OnHyphen()
    {
        InterpretInput("-");
    }
    void OnEquals()
    {
        InterpretInput("=");
    }
    void OnDelete()
    {
        InterpretInput("Back");
    }
    void OnTab()
    {
        InterpretInput("Tab");
    }
    void OnQ()
    {
        InterpretInput("Q");
    }
    void OnE()
    {
        InterpretInput("E");
    }
    void OnR()
    {
        InterpretInput("R");
    }
    void OnT()
    {
        InterpretInput("T");

    }
    void OnY()
    {
        InterpretInput("Y");
    }
    void OnU()
    {
        InterpretInput("U");
    }
    void OnO()
    {
        InterpretInput("O");
    }
    void OnP()
    {
        InterpretInput("P");
    }
    void OnLBracket()
    {
        InterpretInput("[");
    }
    void OnRBracket()
    {
        InterpretInput("]");
    }
    void OnBackSlash()
    {
        InterpretInput("bck slash");
    }
    void OnCapsLock()
    {
        InterpretInput("caps");
    }
    void OnF()
    {
        InterpretInput("F");

    }
    void OnG()
    {
        InterpretInput("G");
    }
    void OnH()
    {
        InterpretInput("H");
    }
    void OnSemicolon()
    {
        InterpretInput(";");
    }
    void OnApostrophe()
    {
        InterpretInput("'");
    }
    void OnReturn()
    {
        InterpretInput("Enter");
    }
    void OnShift()
    {
        InterpretInput("LShift");
    }
    void OnZ()
    {
        InterpretInput("Z");
    }
    void OnX()
    {
        InterpretInput("X");
    }
    void OnC()
    {
        InterpretInput("C");

    }
    void OnV()
    {
        InterpretInput("V");
    }
    void OnB()
    {
        InterpretInput("B");
    }
    void OnN()
    {
        InterpretInput("N");
    }
    void OnM()
    {
        InterpretInput("M");
    }
    void OnComma()
    {
        InterpretInput(",");
    }
    void OnPeriod()
    {
        InterpretInput(".");
    }
    void OnSlash()
    {
        InterpretInput("/");
    }
    void OnRShift()
    {
        InterpretInput("RShift");
    }
    void OnLControl()
    {
        InterpretInput("LControl");

    }
    void OnLAlt()
    {
        InterpretInput("LAlt");
    }
    void OnRAlt()
    {
        InterpretInput("RAlt");
    }
    void OnRCtl()
    {
        InterpretInput("RControl");
    }
    void OnFn()
    {
        InterpretInput("fn");
    }
    void OnHome()
    {
        InterpretInput("home");

    }
    void OnPgup()
    {
        InterpretInput("pgup");
    }
    void OnDel()
    {
        InterpretInput("del");
    }
    void OnEnd()
    {
        InterpretInput("end");
    }
    void OnPgdwn()
    {
        InterpretInput("pgdwn");

    }
    void OnNpdequals()
    {
        InterpretInput("npd =");
    }
    void OnNpdslash()
    {
        InterpretInput("npd /");
    }
    void OnNpdstar()
    {
        InterpretInput("npd *");
    }
    void OnNpd7()
    {
        InterpretInput("npd 7");

    }
    void OnNpd8()
    {
        InterpretInput("npd 8");
    }
    void OnNpdminus()
    {
        InterpretInput("npd -");
    }
    void OnNpd9()
    {
        InterpretInput("npd 9");
    }
    void OnNpd4()
    {
        InterpretInput("npd 4");

    }
    void OnNpd5()
    {
        InterpretInput("npd 5");
    }
    void OnNpd6()
    {
        InterpretInput("npd 6");
    }
    void OnNpdplus()
    {
        InterpretInput("npd +");
    }
    void OnNpd1()
    {
        InterpretInput("npd 1");

    }
    void OnNpd2()
    {
        InterpretInput("npd 2");
    }
    void OnNpd3()
    {
        InterpretInput("npd 3");
    }
    void OnNpd0()
    {
        InterpretInput("npd 0");
    }
    void OnNpddot()
    {
        InterpretInput("npd .");

    }
    void OnNpdenter()
    {
        InterpretInput("npd enter");

    }
    void OnAttack()
    {

        if (receiver.attack)
        {
            receiver.attack = false;
        }
        else
        {
            receiver.attack = true;
            receiver.holdingAttack = 1;
            CycleMovement();
            CycleTapJump();
        }
    }    
    void OnMovement()
    {
        if (receiver.movement)
        {
            receiver.movement = false;
        }
        else
        {
            receiver.movement = true;
            receiver.holdingMovement = 1;
        }
    }
    void OnSpecial()
    {
        if (receiver.special)
        {
            receiver.special = false;
        }
        else
        {
            receiver.special = true;
            receiver.holdingSpecial = 1;
        }
    }
    void OnJump()
    {
        if (receiver.jump)
        {
            receiver.jump = false;
        }
        else
        {
            receiver.jump = true;
            receiver.holdingJump = 1;
        }
    }
    void OnSuper()
    {
        if (receiver.super)
        {
            receiver.super = false;
        }
        else
        {
            receiver.super = true;
            receiver.holdingSuper = 1;
        }
    }
    void OnWebGLAttack()
    {

        if (doubleReceiver.attack)
        {
            doubleReceiver.attack = false;
        }
        else
        {
            doubleReceiver.attack = true;
            doubleReceiver.holdingAttack = 1;
            CycleMovement();
            CycleTapJump();
        }
    }
    void OnWebGLMovement()
    {
        if (doubleReceiver.movement)
        {
            doubleReceiver.movement = false;
        }
        else
        {
            doubleReceiver.movement = true;
            doubleReceiver.holdingMovement = 1;
        }
    }
    void OnWebGLSpecial()
    {
        if (doubleReceiver.special)
        {
            doubleReceiver.special = false;
        }
        else
        {
            doubleReceiver.special = true;
            doubleReceiver.holdingSpecial = 1;
        }
    }
    void OnWebGLJump()
    {
        if (doubleReceiver.jump)
        {
            doubleReceiver.jump = false;
        }
        else
        {
            doubleReceiver.jump = true;
            doubleReceiver.holdingJump = 1;
        }
    }
    void OnWebGLSuper()
    {
        if (doubleReceiver.super)
        {
            doubleReceiver.super = false;
        }
        else
        {
            doubleReceiver.super = true;
            doubleReceiver.holdingSuper = 1;
        }
    }
    void OnAltMovement()
    {
        if (receiver.movement)
        {
            receiver.movement = false;
        }
        else
        {
            receiver.movement = true;
            receiver.holdingMovement = 1;
        }
    }
}