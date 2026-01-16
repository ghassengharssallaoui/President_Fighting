using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuDisconnectControllers : MonoBehaviour
{
    BasicMenuOption bmo;
    public StartTriggerPause godMenu;
    int previousA;
    // Start is called before the first frame update
    void OnEnable()
    {
        bmo = GetComponent<BasicMenuOption>();
        previousA = bmo.aPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (bmo.aPress > previousA)
        {
            previousA = bmo.aPress;
            foreach(GameObject g in godMenu.otherMenus)
            {
                g.GetComponent<VerticalMenu>().player = 1;
            }
            GameObject[] a;
            a = GameObject.Find("InputSpawner").GetComponent<inputHolderForDestroy>().inputs;
            foreach(GameObject g in a)
            {
                if (g != null)
                {
                    Destroy(g);
                }

            }
        }
    }
}
