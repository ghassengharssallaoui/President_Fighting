using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followExactly : MonoBehaviour
{
    public GameObject target;
    public Vector3 targetVect;
    public string targetName;
    public int zDisplacement;
    public bool addPlayerToEndOfTarget;
    public PlayerInfo infoScript;
    void Start()
    {
        if(target != null)
        {
            targetVect = target.transform.position;
        }
        else
        {
            if (addPlayerToEndOfTarget)
            {
                GameObject dummy;
                dummy = gameObject;
                while (dummy.GetComponent<PlayerInfo>() == null)
                {
                    dummy = dummy.transform.parent.gameObject;
                }
                infoScript = dummy.GetComponent<PlayerInfo>();
                if (targetName[targetName.Length-1] != '2' && targetName[targetName.Length - 1] != '2')
                {
                    if (infoScript.player != 0)
                    {
                        targetName += "" + infoScript.player;
                    }
                    else
                    {
                        targetName += "2";

                    }
                }
            }
            if (targetName != "")
            {
                target = GameObject.Find(targetName).gameObject;
                targetVect = target.transform.position;
            }
            else 
            {
                targetVect = transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
           
             targetVect = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + zDisplacement);
            

        }
        transform.position = targetVect;
    }
}
