using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpuLevel : MonoBehaviour
{
    bigEnabler big;
    public int integer;
    // Start is called before the first frame update
    void OnEnable()
    {
        big = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
        big.cpuLevel = integer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
