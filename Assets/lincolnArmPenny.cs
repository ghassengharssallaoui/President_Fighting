using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lincolnArmPenny : MonoBehaviour
{
    public GameObject arm;
    public ReceiveInputs receiver;
    public Options airCoin;
    public Options groundCoin;
    public GameObject coinPrefab;
    public PlayerInfo info;
    public ObamaChargeUI chargeScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        receiver = dummy.GetComponent<PlayerInfo>().receiver;
        info = dummy.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (receiver.holdingMovement == 2 && groundCoin.charges != 0)
        {
            chargeScript.charge = 0;
            groundCoin.charges = 0;
            airCoin.charges = 0;
            if (arm != null)
            {
                arm.active = true;
            }
            GameObject dummy;
            dummy = Instantiate(coinPrefab, new Vector3(info.transform.position.x, info.transform.position.y + coinPrefab.transform.position.y, 0), Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
            dummy.transform.position += new Vector3(coinPrefab.transform.position.x * info.facing, 0, 0);//The reason we do this second is so that (f        }
        }
    }
    void OnDisable()
    {
        if (arm != null)
        {
            arm.active = false;
        }
    }
}
