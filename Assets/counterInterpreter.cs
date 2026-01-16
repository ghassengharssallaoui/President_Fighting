using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterInterpreter : MonoBehaviour
{
    PlayerInfo info;
    public GameObject pose;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        info = dummy.GetComponent<PlayerInfo>();
    }
    // Update is called once per frame
    void Update()
    {
        if(info.hit == 2)
        {
            pose.active = true;
            FaceEnemy();
            gameObject.active = false;
        }
    }
    void FaceEnemy()
    {
        int dummyint;
        float dummyFloat;
        dummyFloat = -1 * (Mathf.Abs(info.gameObject.transform.position.x - info.enemyObject.transform.position.x) / (info.gameObject.transform.position.x - info.enemyObject.transform.position.x));
        if (dummyFloat > 0)
        {
            dummyint = 1;
        }
        else
        {
            dummyint = -1;
        }
        transform.localScale = new Vector3(dummyint, 1, 1);
        info.facing = dummyint;
    }
}
