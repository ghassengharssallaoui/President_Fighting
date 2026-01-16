using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuUpdatePrefOnRight : MonoBehaviour
{
    BasicMenuOption bmo;
    int previousR;
    int previousL;
    public string prefName;
    public int maxValue;
    public int minValue;
    public int incrementValue;
    public GameObject SFX;
    // Start is called before the first frame update
    void OnEnable()
    {
        bmo = GetComponent<BasicMenuOption>();
        previousR = bmo.rPress;
        previousL = bmo.lPress;
    }
    void InstantiateSFX()
    {
        if (SFX != null)
        {
            Instantiate(SFX);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (bmo.hovering == false)
        {
            OnEnable();
        }
        if (bmo.rPress > previousR)
        {
            previousR = bmo.rPress;
            int valuetToChange = PlayerPrefs.GetInt(prefName);
            if(valuetToChange + incrementValue > maxValue)
            {
                PlayerPrefs.SetInt(prefName, maxValue);
            }
            else
            {
                PlayerPrefs.SetInt(prefName, valuetToChange + incrementValue);
            }
            InstantiateSFX();
        }
        if (bmo.lPress > previousL)
        {
            previousL = bmo.lPress;
            int valuetToChange = PlayerPrefs.GetInt(prefName);
            if(valuetToChange - incrementValue < minValue)
            {
                PlayerPrefs.SetInt(prefName, minValue);
            }
            else
            {
                PlayerPrefs.SetInt(prefName, valuetToChange - incrementValue);
            }
            InstantiateSFX();
        }
    }
}
