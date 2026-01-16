using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableOnBigEnable : MonoBehaviour
{
    public bigEnabler big;
    public int bigCounter;
    void OnEnable()
    {
        big = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
    }
    void BigDisable()
    {
        if (big.doEnable || big.fakeEnable)
        {
            print(name);
            if (bigCounter == 2)//the two and three is to stop it from happening frame one, which breaks the game.
            {
                transform.root.gameObject.active = false;
            }
        } 
        if (bigCounter < 2)
        {
            bigCounter += 1;
        }
    }
    void Update()
    {
        BigDisable();
    }
}
