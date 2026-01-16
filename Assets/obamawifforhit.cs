using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obamawifforhit : MonoBehaviour
{
    public GameObject super;
    public GameObject wiff;
    public GameObject superHitBox;
    public GameObject gray;
    public GameObject gray2;

    PlayerInfo infoScript;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        if (infoScript.enemyScript.hit == -2)
        {
            infoScript.currentAnim = super;
            super.active = true;
            Time.timeScale = 1;
            gray2.active = true;
            gray.active = false;
        }
        else
        {
            wiff.active = true;
        }
        superHitBox.active = false;
        gameObject.active = false;
    }

        // Update is called once per frame
    void Update()
    {
        
    }
}
