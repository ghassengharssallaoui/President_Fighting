using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteEnabler : MonoBehaviour
{
    public SpriteRenderer sprite;
    public bool done;
    public PlayerInfo infoScript;
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        done = false;
    }
    // Start is called before the first frame update
    void Update()
    {
        if (sprite != null)
        {
            if (done == false)
            {
                sprite.enabled = true;
                if (infoScript.characterID == infoScript.enemyScript.characterID && infoScript.player == 2)
                {
                    sprite.color = new Vector4(1, 0, 0, 1);
                }
                else
                {
                    sprite.color = new Vector4(1, 1, 1, 1);

                }
                done = true;
            }
        }
    }
}
