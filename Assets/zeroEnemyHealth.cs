using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zeroEnemyHealth : MonoBehaviour
{
    public bool onEnable;
    public bool onDisable;
    public bool suicide;
    public int rate;
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
        if (onEnable)
        {
            ZeroHealth();
        }
    }
    void ZeroHealth()
    {
        if (suicide)
        {
            info.zeroHealth = true;
            info.zeroHealthRate = rate;
            
        }
        info.enemyScript.zeroHealth = true;
        info.enemyScript.zeroHealthRate = rate;
    }
    // Update is called once per frame
    void OnDisable()
    {
        if (onDisable)
        {
            ZeroHealth();
        }
    }
}
