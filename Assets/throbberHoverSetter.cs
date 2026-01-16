using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throbberHoverSetter : MonoBehaviour
{
    public p1SelectorThrobber selector;
    public int player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == 1)
        {
            selector.p1 = true;
        }
        else
        {
            selector.p2 = true;
        }
    }
    void OnDisable()
    {
        if (player == 1)
        {
            selector.p1 = false;
        }
        else
        {
            selector.p2 = false;
        }
    }
}
