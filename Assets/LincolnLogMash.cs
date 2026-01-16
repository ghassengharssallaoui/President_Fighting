using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LincolnLogMash : MonoBehaviour
{
    PlayerInfo info;
    ReceiveInputs inputs;
    float xDrift;
    public Vector3 BaseSpeed;
    public Vector3 BonusSpeed;
    int bonusTimer;
    public GameObject sfx;
    public bool ground;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (info == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<PlayerInfo>();
            inputs = info.receiver;
        }
        bonusTimer = 0;
        Instantiate(sfx, transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dummyVect;
        dummyVect = new Vector3(BaseSpeed.x * inputs.moveVector.x, BaseSpeed.y, 0);
        if(inputs.holdingSpecial == 2)
        {
            Instantiate(sfx, transform.position, Quaternion.identity);
            if(bonusTimer > 0)
            {
                bonusTimer = 3;
            }
            else
            {
                bonusTimer = 4;
            }
        }
        if (bonusTimer == 4 || bonusTimer == 1)
        {
            bonusTimer += -1;
            dummyVect += new Vector3(BonusSpeed.x * inputs.moveVector.x, BonusSpeed.y * 0.5f, 0);

        }
        else if (bonusTimer > 0)
        {
            bonusTimer += -1;
            dummyVect += new Vector3(BonusSpeed.x * inputs.moveVector.x, BonusSpeed.y, 0);
        }
        info.traj = dummyVect;
    }
}
