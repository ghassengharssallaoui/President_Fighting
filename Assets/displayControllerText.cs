using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayControllerText : MonoBehaviour
{
    Text t;
    inputHolderForDestroy inputs;
    public int player;
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Text>();
        inputs = GameObject.Find("InputSpawner").GetComponent<inputHolderForDestroy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputs.inputs[player - 1] == null)
        {
            t.text = "Press Start! (escape)";
        }
        else
        {
            t.text = "Player " + player + " Connected";
        }
    }
}
