using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipJFKCinematic : MonoBehaviour
{
    public ReceiveInputs input;
    public int skippy;
    public int lengthOfTimeSkip;
    public int counter;
    int trueCounter;
    public GameObject skipText;
    // Start is called before the first frame update
    void OnEnable()
    {
        trueCounter = 0;
        counter = 0;
        skippy = 0;
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        if(dummy.GetComponent<PlayerInfo>().player ==1)
        {
            input = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        }
        else
        {
            input = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trueCounter += 1;
        if(trueCounter == 60 && skippy == 0)
        {
            skipText.active = true;
        }
        if(skippy == 1)
        {
            skippy = 2;
            GetComponent<AudioSource>().enabled = false;
            skipText.active = false;
        }
        if(skippy == 2)
        {
            Time.timeScale = 10;
        }
        if(trueCounter == lengthOfTimeSkip)
        {
            gameObject.active = false;
        }
        if(input.holdingAttack == 2)
        {
            skippy = 1;
        }
        print(Time.timeScale);
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
