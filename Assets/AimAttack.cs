using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAttack : MonoBehaviour
{
    public GameObject[] validAnimations;
    public GameObject Up;
    public GameObject UpLeft;
    public GameObject UpRight;
    public GameObject Right;
    public GameObject Left;
    public GameObject Down;
    public GameObject DownRight;
    public GameObject DownLeft;
    public bool deadAfterUse;
    PlayerInfo infoScript;
    ReceiveInputs inputs;
    // Start is called before the first frame update
    void BullshitForChargesOnShotGun()
    {
        if (GetComponent<Options>() != null)
        {
            Options o = GetComponent<Options>();
            o.charges += -1;
            if (o.sharedCharges.Length > 0)
            {
                foreach (Options p in o.sharedCharges)
                {
                    p.charges = o.charges;
                }
            }
        }
    }
    void OnEnable()
    {
        if (infoScript == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            infoScript = dummy.GetComponent<PlayerInfo>();
            inputs = infoScript.receiver;
        }
        BullshitForChargesOnShotGun();
        changeAnim();
        if (deadAfterUse == false)
        {
            if (checkdeactivate())
            {
                if (infoScript.facing == 1)
                {
                    setAnim(Right);


                }
                else
                {
                    setAnim(Left);
                }
            }
        }
        else
        {
            if (inputs.holding[0] <= 0 && inputs.holding[1] <= 0 && inputs.holding[2] <= 0 && inputs.holding[3] <= 0)
            {
                if (infoScript.facing == 1)
                {
                    setAnim(Right);
                }
                else
                {
                    setAnim(Left);
                }
            }
            gameObject.active = false;
        }
    }
    void setAnim(GameObject g)
    {
        if (g != null)
        {
            if (g.active != true || (infoScript.facing == 1 && inputs.holding[2] > 0) || (infoScript.facing == -1 && inputs.holding[3] >0))
            {
                if (Up != null)
                {
                    Up.active = false;
                }
                if (Down != null)
                {
                    Down.active = false;
                }
                if (Right != null)
                {
                    Right.active = false;
                }
                if (Left != null)
                {
                    Left.active = false;
                }
                if (UpLeft != null)
                {
                    UpLeft.active = false;
                }
                if (UpRight != null)
                {
                    UpRight.active = false;
                }
                if (DownRight != null)
                {
                    DownRight.active = false;
                }
                if (DownLeft != null)
                {
                    DownLeft.active = false;
                }
                g.active = true;
            }
        }
    }
    void changeAnim()
    {
        if (inputs.holding[0] > 0)
        {
            if (inputs.holding[2] > 0)
            {
                setAnim(UpLeft);
            }
            else if (inputs.holding[3] > 0)
            {
                setAnim(UpRight);
            }
            else
            {
                setAnim(Up);
            }
        }else if (inputs.holding[1] > 0)
        {
            if (inputs.holding[2] > 0)
            {
                setAnim(DownLeft);
            }
            else if (inputs.holding[3] > 0)
            {
                setAnim(DownRight);
            }
            else
            {
                setAnim(Down);
            }
        }
        else
        {
            if (inputs.holding[2] > 0)
            {
                setAnim(Left);
            }
            if (inputs.holding[3] > 0)
            {
                setAnim(Right);
            }
        }
    }
    bool checkdeactivate()
    {
        foreach(GameObject g in validAnimations)
        {
            if (g.active)
            {
                return true;
            }
        }
        return false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(infoScript.hit >= 0)
        {
            changeAnim();
        }
        else
        {
            gameObject.active = false;
        }
        if (checkdeactivate() == false)
        {
            gameObject.active = false;
        }
    }
}
