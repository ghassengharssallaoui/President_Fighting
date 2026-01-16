using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LincolnWinLoseDecider : MonoBehaviour
{
    PlayerInfo info;
    public GameObject winAnim;
    public GameObject loseAnim;
    public GameObject deadLincoln;
    public GameObject rocks;
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
        Instantiate(deadLincoln, info.transform.position, Quaternion.identity);

        if (info.enemyScript.health == -1)
        {
            winAnim.active = true;

        }
        else
        {
            loseAnim.active = true;
        }
        for(int i = 0; i < 5; i++)
        {
            Instantiate(rocks, new Vector3(transform.position.x, 20, 0), Quaternion.identity);
        }
        gameObject.active = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
