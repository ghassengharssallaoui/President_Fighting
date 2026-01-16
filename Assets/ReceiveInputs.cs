using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveInputs : MonoBehaviour
{
    public bool online;
    public ReceiveInputs feeder;
    public int lastLeft;//-1 = left. +1 = right 0 = held too long
    public InputGod myInput;
    //WHEN YOU RETURN: Need to update the "holding" feature and retool the buttons. I'm thinking maybe it's better to just have it be and "Attack" for now cus idk how remmapping works. Also find out how remapping works
    // Update is called once per frame
    public int[] holding = new int[4]; //up down left right
    public bool start;
    public bool attack;
    public bool special;
    public bool jump;
    public bool movement;
    public bool super;
    public int holdingStart;
    public int holdingAttack;
    public int holdingSpecial;
    public int holdingJump;
    public int holdingMovement;
    public int holdingSuper;
    public bool tapJump;
    public Vector2 moveVector;
    public Vector2 lastMoveVector;
    /////
    public int TRUEholdingStart;
    public int TRUEholdingAttack;
    public int TRUEholdingSpecial;
    public int TRUEholdingJump;
    public int TRUEholdingMovement;
    public int TRUEholdingSuper;
    public int[] TRUEholding = new int[4]; //up down left right

    // Start is called before the first frame update
    void Start()
    {
    }
    void LateUpdate()
    {
        lastMoveVector = moveVector;
    }
    int GetInput(float value)
    {
        if (Mathf.Abs(value) <= 0.5)
        {
            return 0;
        }
        else
        {
            return (int)(value / Mathf.Abs(value));
        }
    }
    void resetInputs()
    {
        if (GetInput(moveVector.x) == 1)
        {
            holding[3] += 1;
            holding[2] = 0;
            if (lastLeft < 0 || holding[3] <= 3)
            {
                lastLeft = 1;
            }

        }
        else if (GetInput(moveVector.x) == -1)
        {
            holding[3] = 0;
            holding[2] += 1;
            if (lastLeft > 0 || holding[2] <= 3)
            {
                lastLeft = -1;
            }

        }
        else
        {
            holding[2] = 0;
            holding[3] = 0;
        }
        if(lastLeft > 0)
        {
            lastLeft += 1;
        }
        if(lastLeft < 0)
        {
            lastLeft += -1;
        }
        if(lastLeft == 0)
        {
            lastLeft = 10000;
        }
        if (GetInput(moveVector.y) == 1)
        {
            holding[0] += 1;
            holding[1] = 0;
        }
        else if (GetInput(moveVector.y) == -1)
        {
            holding[0] = 0;
            holding[1] += 1;
        }
        else
        {
            holding[0] = 0;
            holding[1] = 0;
        }
        if (start)
        {
            holdingStart += 1;
        }
        else
        {
            if (holdingStart != 1)
            { 
                holdingStart = 0;
            }
            else
            {
                holdingStart += 1;
            }
        }
        if (attack)
        {
            holdingAttack += 1;
        }
        else
        {
            if (holdingAttack != 1)
            {
                holdingAttack = 0;
            }
            else
            {
                holdingAttack += 1;
            }
        }
        if (jump || (tapJump && holding[0] > 0 && moveVector.y > 0.7))
        {
            holdingJump += 1;
        }
        else
        {
            if (holdingJump != 1)
            {
                holdingJump = 0;
            }
            else
            {
                holdingJump += 1;
            }
        }        
        if (movement)
        {
            holdingMovement += 1;
        }
        else
        {
            if (holdingMovement != 1)
            {
                holdingMovement = 0;
            }
            else
            {
                holdingMovement += 1;
            }
        }
        if (super)
        {
            holdingSuper += 1;
        }
        else
        {
            if (holdingSuper != 1)
            {
                holdingSuper = 0;
            }
            else
            {
                holdingSuper += 1;
            }
        }
        if (special)
        {
            holdingSpecial += 1;
        }
        else
        {
            if (holdingSpecial != 1)
            {
                holdingSpecial = 0;
            }
            else
            {
                holdingSpecial += 1;
            }
        }        
    }
    void TRUEresetInputs() //this is an update version of the same script for inputs during pause
    {
        if (GetInput(moveVector.x) == 1)
        {
            TRUEholding[3] += 1;
            TRUEholding[2] = 0;
        }
        else if (GetInput(moveVector.x) == -1)
        {
            TRUEholding[3] = 0;
            TRUEholding[2] += 1;
        }
        else
        {
            TRUEholding[2] = 0;
            TRUEholding[3] = 0;
        }
        if (GetInput(moveVector.y) == 1)
        {
            TRUEholding[0] += 1;
            TRUEholding[1] = 0;
        }
        else if (GetInput(moveVector.y) == -1)
        {
            TRUEholding[0] = 0;
            TRUEholding[1] += 1;
        }
        else
        {
            TRUEholding[0] = 0;
            TRUEholding[1] = 0;
        }
        if (start)
        {
            TRUEholdingStart += 1;
        }
        else
        {
            if (TRUEholdingStart != 1)
            {
                TRUEholdingStart = 0;
            }
            else
            {
                TRUEholdingStart += 1;
            }
        }
        if (attack)
        {
            TRUEholdingAttack += 1;
        }
        else
        {
            if (TRUEholdingAttack != 1)
            {
                TRUEholdingAttack = 0;
            }
            else
            {
                TRUEholdingAttack += 1;
            }
        }
        if (jump || (tapJump && holding[0] > 0 && moveVector.y > 0.7))
        {
            TRUEholdingJump += 1;
        }
        else
        {
            if (holdingJump != 1)
            {
                TRUEholdingJump = 0;
            }
            else
            {
                TRUEholdingJump += 1;
            }
        }
        if (movement)
        {
            TRUEholdingMovement += 1;
        }
        else
        {
            if (holdingMovement != 1)
            {
                TRUEholdingMovement = 0;
            }
            else
            {
                TRUEholdingMovement += 1;
            }
        }
        if (super)
        {
            TRUEholdingSuper += 1;
        }
        else
        {
            if (TRUEholdingSuper != 1)
            {
                TRUEholdingSuper = 0;
            }
            else
            {
                TRUEholdingSuper += 1;
            }
        }
        if (special)
        {
            TRUEholdingSpecial += 1;
        }
        else
        {
            if (TRUEholdingSpecial != 1)
            {
                TRUEholdingSpecial = 0;
            }
            else
            {
                TRUEholdingSpecial += 1;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.timeScale != 0.00001f)
        {
            resetInputs();
        }
    }
    void Update()
    {
        TRUEresetInputs();
    }
}
