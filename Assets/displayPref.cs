using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayPref : MonoBehaviour
{
    public string prefName;
    public bool intPref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (intPref) {
            GetComponent<Text>().text = "" + PlayerPrefs.GetInt(prefName);
    }
    }
}
