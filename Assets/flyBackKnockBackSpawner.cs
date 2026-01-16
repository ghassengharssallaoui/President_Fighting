using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyBackKnockBackSpawner : MonoBehaviour
{
    public GameObject effect;
    public PlayerInfo infoScript;
    void OnEnable()
    {
        GameObject dummy;
        dummy = transform.parent.gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        if(infoScript.hit == -1)
        {
            Vector3 vect;
            vect = new Vector3((infoScript.transform.position.x + infoScript.enemyScript.transform.position.x) / 2, 3+ (infoScript.transform.position.y + infoScript.enemyScript.transform.position.y) / 2, 0);
            Instantiate(effect, vect, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
