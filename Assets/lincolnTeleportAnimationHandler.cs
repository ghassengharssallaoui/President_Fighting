using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lincolnTeleportAnimationHandler : MonoBehaviour
{
    public bool teleport;
    public GameObject[] normalAnims;
    public GameObject[] specialAnims;
    public GameObject currentAnim;
    // Start is called before the first frame update
    void AnimationChange()
    {
        for(int i= 0; i < normalAnims.Length; i++)
        {
            if (normalAnims[i].active == true)
            {
                if (normalAnims[i].GetComponent<Options>() != null)
                {
                    specialAnims[i].GetComponent<Options>().currentAnimFrame = normalAnims[i].GetComponent<Options>().currentAnimFrame;
                    specialAnims[i].GetComponent<Options>().currentAnim = normalAnims[i].GetComponent<Options>().currentAnim;
                    specialAnims[i].GetComponent<Options>().currentFrame = normalAnims[i].GetComponent<Options>().currentFrame;
                    specialAnims[i].GetComponent<Options>().dontReset = true;
                }
                specialAnims[i].active = true;
                normalAnims[i].active = false;
                currentAnim = specialAnims[i];
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(teleport == true)
        {
            if (transform.position.y > 0)
            {
                AnimationChange();
            }
            teleport = false;

        }
        if (currentAnim != null)
        {
            if(currentAnim.active == false)
            {
                print("zeroing");
                currentAnim.GetComponent<Options>().dontReset = false;
                currentAnim.GetComponent<Options>().currentAnim = 0;
                currentAnim.GetComponent<Options>().currentAnimFrame = 0;
                currentAnim.GetComponent<Options>().currentFrame = 0;
                currentAnim = null;
            }
        }
    }
}
