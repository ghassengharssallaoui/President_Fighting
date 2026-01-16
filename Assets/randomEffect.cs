using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomEffect : MonoBehaviour
{
    public GameObject[] effects;
    // Start is called before the first frame update
    void OnEnable()
    {
        int a = Random.Range(0, effects.Length);
        for(int i =0; i < effects.Length; i++)
        {
            if(i == a)
            {
                effects[i].active = true;
            }
            else
            {
                effects[i].active = false;
            }
        }
        GetComponent<randomEffect>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
