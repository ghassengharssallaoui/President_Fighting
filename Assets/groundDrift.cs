using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundDrift : MonoBehaviour
{
    public float speed;
    ReceiveInputs inputs;
    PlayerInfo info;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<findPlayer>() != null)
        {
            info = GetComponent<findPlayer>().player;
            inputs = GetComponent<findPlayer>().player.receiver;
        }
        if (GetComponent<Options>() != null)
        {
            info = GetComponent<Options>().infoScript;
            inputs = GetComponent<Options>().infoScript.receiver;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.holding[2] > 2)
        {
            info.traj = new Vector3(-speed, info.traj.y, 0);
        }
        if (inputs.holding[3] > 2)
        {
            info.traj = new Vector3(speed, info.traj.y, 0);

        }
    }
}
