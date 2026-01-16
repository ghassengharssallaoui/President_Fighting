using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectileSpawner : MonoBehaviour
{
    public PlayerInfo info;
    GameObject godObject;
    public GameObject prefab;
    public Vector3 prefabRotation;
    public GameObject dontSpawnFrame;
    // Start is called before the first frame update
    void OnEnable()
    {
        bool doit = true;
        if(dontSpawnFrame != null)
        {
            if(dontSpawnFrame.active == true)
            {
                doit = false;
            }
        }
        if (doit)
        {
            if (info == null)
            {
                GameObject dummy;
                dummy = gameObject;
                while (dummy.GetComponent<PlayerInfo>() == null)
                {
                    dummy = dummy.transform.parent.gameObject;
                }
                godObject = dummy;
                info = dummy.GetComponent<PlayerInfo>();
            }
            else
            {
                godObject = info.transform.gameObject;
            }
            Vector3 dummyVect;
            if(info.facing == 1 && prefabRotation.z != 0)
            {
                
                dummyVect = new Vector3(prefabRotation.x, prefabRotation.y, prefabRotation.z * -1);
            }
            else
            {
                dummyVect = prefabRotation;
            }
            GameObject clone = Instantiate(prefab, new Vector3(godObject.transform.position.x + prefab.transform.position.x * info.facing, godObject.transform.position.y + prefab.transform.position.y, 0), Quaternion.Euler(dummyVect));
            clone.GetComponent<hitBoxAssigner>().info = info;
            clone.transform.localScale = new Vector3(info.facing * clone.transform.localScale.x, clone.transform.localScale.y, 1);
            clone.GetComponent<JustAnimate>().info = info;
        }   
    }

    // Start is called before the first frame update
}
