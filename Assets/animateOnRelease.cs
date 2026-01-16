using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateOnRelease : MonoBehaviour
{
    public bool Attack;
    public bool Special;
    public bool Super;
    public bool Movement;
    public bool Jump;
    public GameObject[] deactivate;
    public GameObject[] activate;
    PlayerInfo infoScript;
    ReceiveInputs inputs;
    // Start is called before the first frame update
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
    }
    void Next()
    {
        foreach(GameObject g in activate)
        {
            g.active = true;
        }
        foreach (GameObject g in deactivate)
        {
            g.active = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (
            (Jump == true && inputs.holdingJump <= 0) ||
            (Attack == true && inputs.holdingAttack <= 0) ||
            (Super == true && inputs.holdingSuper <= 0) ||
            (Special == true && inputs.holdingSpecial <= 0) ||
            (Movement == true && inputs.holdingMovement <= 0)
            )
        {
            Next();
        }
    }
}
