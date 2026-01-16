using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateEnemyVictoryPose : MonoBehaviour
{
    PlayerInfo info;
    int counter;
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
        counter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter += 1;
        if (counter == 20 || counter == 21)
        {
            info.enemyScript.hit = 0;
            info.enemyScript.currentAnim.active = false;
            info.enemyScript.victoryAnim.active = true;
        }
    }
}
