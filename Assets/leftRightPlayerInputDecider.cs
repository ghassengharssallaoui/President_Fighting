using UnityEngine;

public class leftRightPlayerInputDecider : MonoBehaviour
{
     PlayerInfo infoScript;
     ReceiveInputs inputs;
    public GameObject forwardOption;
    public GameObject backOption;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        int facing;
        int dir;
        if(infoScript.transform.localScale.x > 0)
        {
            facing = 1;
        }
        else
        {
            facing = -1;
        }
        if (inputs.holding[2] > 0)
        {
            dir = -1;
        }else if (inputs.holding[3] > 0)
        {
            dir = 1;
        }
        else
        {
            dir = facing;
        }
        if(dir == facing)
        {
            forwardOption.active = true;
            gameObject.active = false;
        }
        else
        {
            backOption.active = true;
            gameObject.active = false;
        }
    }
}
