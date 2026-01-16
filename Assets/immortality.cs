using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class immortality : MonoBehaviour
{
    public bool falseProphetMarkNameWithF;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (falseProphetMarkNameWithF)
        {
            if (GameObject.Find(gameObject.name.Substring(0, gameObject.name.Length - 1)) != null)
            {
                Destroy(transform.root.gameObject);
            }
            else
            {
                if (gameObject.name[gameObject.name.Length - 1] == 'f')
                {
                    gameObject.name = gameObject.name.Substring(0, gameObject.name.Length - 1);
                }
                DontDestroyOnLoad(gameObject);

            }
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
