using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preventUpperCutAfterCancel : MonoBehaviour
{
    public ObamaChargeUI script;
    public Options uppercut;
    public Options airUppercut;
    public Options tatsu;
    public int delayFrames;
    public int oldCharges;
    public int tatsuCharges;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(script.charge == 2)
        {
            tatsu.charges = 1;
        }
        //print(airUppercut.charges);
        if (script.charge < delayFrames)
        {

                oldCharges = 1;
                airUppercut.charges = 0;
                uppercut.charges = 0;
            
        }
        else
        {
            if (oldCharges == 1)
            {
                airUppercut.charges = 1;
                uppercut.charges = 1;
            }
            oldCharges = 0;
        }
        
    }
}
