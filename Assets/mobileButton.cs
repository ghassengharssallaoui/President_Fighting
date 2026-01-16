using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mobileButton : MonoBehaviour
{
    public Button yourButton;
    ReceiveInputs receiver;
    public string button;
    void Start()
    {
        receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
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
        }
    }
    void TaskOnClick()
    {
        if(button == "A")
        {
            OnAttack();
        }
        if (button == "X")
        {
            OnSpecial();
        }
        if (button == "Y")
        {
            OnMovement();
        }
        if (button == "B")
        {
            OnSuper();
        }

    }
    void Update()
    {
        if (receiver.holdingSuper == 3)
        {
            receiver.holdingSuper = 0;
            receiver.super = false;
        }
        if (receiver.holdingAttack == 3)
        {
            receiver.holdingAttack = 0;
            receiver.attack = false;
        }
        if (receiver.holdingMovement == 3)
        {
            receiver.holdingMovement = 0;
            receiver.movement = false;
        }
        if (receiver.holdingSpecial == 3)
        {
            receiver.holdingSpecial = 0;
            receiver.special = false;
        }
    }
}