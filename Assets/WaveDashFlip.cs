using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDashFlip : MonoBehaviour
{
    public GameObject backDashFrame;
    public GameObject turningFrame;
    PlayerInfo info;
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
        }
        if (backDashFrame.active == true || turningFrame.active == true)
        {
            info.facing = info.facing * -1;
        }
    }

}
