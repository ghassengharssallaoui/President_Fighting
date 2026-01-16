using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIcon : MonoBehaviour
{
    public GameObject[] p1hoverobjects;
    public GameObject[] p2hoverobjects;
    public GameObject[] p1selectobjects;
    public GameObject[] p2selectobjects;
    public GameObject[] p1confirmobjects;
    public GameObject[] p2confirmobjects;
    public int p1; //0 = none, 1 hover, 2 select, 3 confirm...
    public int p2;
    MenuManager mm;
    bigEnabler be;
    public GameObject characterAsset;
    public int p1counter;
    public int p2counter;
    //this will also store all of the relevant information that needs to be made alive upon p1 or p2 hovering
    //

    // Start is called before the first frame update
    void P1Hover()
    {
        //placeholder function for anything else I need to set up. Animations, etc.
    }
    void P2Hover()
    {
    }
    void P1Select()
    {
        //placeholder function for anything else I need to set up. Animations, etc.
    }
    void P2Select()
    {

    }
    void P1Confirm()
    {
        //placeholder function for anything else I need to set up. Animations, etc.
    }
    void P2Confirm()
    {

    }
    ///public GameObject menu1;

    // Start is called before the first frame update
    void Awake()
    {
        mm = GameObject.Find("Player Select Manager").GetComponent<MenuManager>();
        be = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
    }
    // Update is called once per frame
    void Update()
    {
        if(p1 > 0)
        {
            be.p1asset = characterAsset;
        }
        if(p2 > 0)
        {
            be.p2asset = characterAsset;
        }
        if (p1 == 1)
        {
            if (p1counter == 0)
            {
                foreach (GameObject g in p1selectobjects)
                {
                    g.active = false;
                }
                foreach (GameObject g in p1confirmobjects)
                {
                    g.active = false;
                }
                foreach (GameObject g in p1hoverobjects)
                {
                    g.active = true;
                }

                P1Hover();
            }
        }
        else if (p1 == 2)
        {
                if (p1counter == 0)
                {
                    foreach (GameObject g in p1hoverobjects)
                    {
                        g.active = false;
                    }
                    foreach (GameObject g in p1confirmobjects)
                    {
                        g.active = false;
                    }
                    foreach (GameObject g in p1selectobjects)
                    {
                        g.active = true;
                    }

                    P1Select();
                }
        }
        else if (p1 == 3)
        {
                    if (p1counter == 0)
                    {
                        foreach (GameObject g in p1hoverobjects)
                        {
                            g.active = false;
                        }
                        foreach (GameObject g in p1selectobjects)
                        {
                            g.active = false;
                        }
                        foreach (GameObject g in p1confirmobjects)
                        {
                            g.active = true;
                        }
                        P1Confirm();
                    }
        }
        else
        {
            foreach (GameObject g in p1hoverobjects)
            {
                g.active = false;
            }
            foreach (GameObject g in p1selectobjects)
            {
                g.active = false;
            }
            foreach (GameObject g in p1confirmobjects)
            {
                g.active = false;
            }
        }
        if (p2 == 1)
        {
            if (p2counter == 0)
            {
                foreach (GameObject g in p2hoverobjects)
                {
                    g.active = true;
                }
                foreach (GameObject g in p2selectobjects)
                {
                    g.active = false;
                }
                foreach (GameObject g in p2confirmobjects)
                {
                    g.active = false;
                }
                P2Hover();
            }
        }
        else if (p2 == 2)
        {
            if (p2counter == 0)
            {
                foreach (GameObject g in p2hoverobjects)
                {
                    g.active = false;
                }
                foreach (GameObject g in p2selectobjects)
                {
                    g.active = true;
                }
                foreach (GameObject g in p2confirmobjects)
                {
                    g.active = false;
                }
                P2Select();
            }
        }
        else if (p2 == 3)
        {
            if (p2counter == 0)
            {
                foreach (GameObject g in p2hoverobjects)
                {
                    g.active = false;
                }
                foreach (GameObject g in p2selectobjects)
                {
                    g.active = false;
                }
                foreach (GameObject g in p2confirmobjects)
                {
                    g.active = true;
                }
                P2Confirm();
            }
        }
        else
        {
            foreach (GameObject g in p2hoverobjects)
            {
                g.active = false;
            }
            foreach (GameObject g in p2selectobjects)
            {
                g.active = false;
            }
            foreach (GameObject g in p2confirmobjects)
            {
                g.active = false;
            }
        }
        p1counter += 1;
        p2counter += 1;
    }
}
