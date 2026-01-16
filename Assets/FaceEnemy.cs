using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceEnemy : MonoBehaviour
{
    PlayerInfo info;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        info = dummy.GetComponent<PlayerInfo>();
        if(info.transform.position.x < info.enemyScript.transform.position.x && info.facing != 1)
        {
            info.transform.localScale = new Vector3(1, 1, 0);
            info.facing = 1;
        }
        if (info.transform.position.x > info.enemyScript.transform.position.x && info.facing != -1)
        {
            info.transform.localScale = new Vector3(-1, 1, 0);
            info.facing = -1;
        }
    }

}
