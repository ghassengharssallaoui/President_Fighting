using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPartyCharges : MonoBehaviour
{
    public ThirdPartyCharges sharedWith;
    public int charges;
    OptionsReference reference;
    public GameObject[] activate;
    public GameObject[] deactivate;
    public bool deactivateSelfActivateDefaultReference = true;
    public bool air;
    public GameObject testObject;
    public int doit;//this is so the script will wait one true frame before running.
    // Start is called before the first frame update
    void OnEnable()
    {
        if (reference == null)
        {
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<OptionsReference>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            reference = dummy.GetComponent<OptionsReference>();
        }
        doit = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Options>().charges = charges;
        if (doit == 0 && false)
        {
            if (sharedWith != null)
            {
                charges = sharedWith.charges;
            }
            if (charges > 0)
            {
                foreach (GameObject g in activate)
                {
                    g.active = true;

                }
                foreach (GameObject g in deactivate)
                {
                    g.active = false;
                }
                if (deactivateSelfActivateDefaultReference)
                {
                    gameObject.active = false;
                }
                if (sharedWith != null)
                {
                    sharedWith.charges += -1;
                }
                else
                {
                    charges += -1;
                }
            }
            else
            {
                gameObject.active = false;

                if (deactivateSelfActivateDefaultReference)
                {
                    testObject = reference.groundDefault;
                    if (air)
                    {
                        reference.airDefault.active = true;
                    }
                    else
                    {
                        print("I'm trying");
                        reference.groundDefault.active = true;
                    }
                }
            }
        }
        doit += -1;

    }
}
