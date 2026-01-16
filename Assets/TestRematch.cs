using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRematch : MonoBehaviour
{
    ReceiveInputs p1Input;
    ReceiveInputs p2Input;

    public GameObject charSelect;
    public GameObject cam;
    BetterCameraMovement camScript;
    public bigEnabler bigEnable;
    bool unkillable;
    // Start is called before the first frame update
    void OnEnable()
    {
        unkillable = true;
        p1Input = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        p2Input = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
        camScript = cam.GetComponent<BetterCameraMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (unkillable == false)
        {
            if (p1Input.holding[0] == 2 || p1Input.holding[1] == 2 || p2Input.holding[0] == 2 || p2Input.holding[1] == 2)
            {
                charSelect.active = true;
                gameObject.active = false;
            }
        }
        unkillable = false;
        if (p1Input.holdingAttack == 2 || p2Input.holdingAttack == 2)
        {
            GameObject p1 = camScript.p1;
            GameObject p2 = camScript.p2;
            Destroy(p1);
            Destroy(p2);
            foreach (GameObject g in bigEnable.unorderedStuffToEnable)
            {
                g.active = false;
            }
            bigEnable.doEnable = true;
            Time.timeScale = 1;
            gameObject.active = false;
        }
    }
}
