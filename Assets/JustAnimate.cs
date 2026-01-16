using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustAnimate : MonoBehaviour
{
    public bool loop; //
    public bool dontDestroy;
    public bool dontDeactivate;
    public GameObject next; //if null will go to idle in reference.
    public int currentFrame; //total frame
    public int currentAnimFrame;
    public int currentAnim;
    public anim[] frames;
    public GameObject sfxPrefab;
    public PlayerInfo info; //setbyprojectilespawners or otherwise
    public Vector3[] stop_destroy_otherOnHitArray = new Vector3[20];
    public Vector3[] stop_destroy_otherOnHitArrayNEGATIVE = new Vector3[20];//lazy way to deal with the hit animations. 1 means destroy or stop. 
                        /// <summary>
                        /// 1 on x and y means that it's a "basic hit" ie. a hit that needs to be on screen only until the hit resolves
                        /// </summary>
    bool killBasicAttack;
    public bool enableReset; //I have no fucking idea why we can't ALWAYS do this, but I've chosen not to question it.
    public bigEnabler big;
    public bool dontBigReset;
    public int bigCounter;
    void BigDisable()
    {
        if (dontBigReset == false)
        {
            if (big.doEnable || big.fakeEnable)
            {
                transform.root.gameObject.active = false;
            }

        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.AddComponent<networkSprite>();
        big = GameObject.Find("BigEnabler").GetComponent<bigEnabler>();
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].spriteObject == null)
            {
                break;
            }
            frames[i].sprite = frames[i].spriteObject.GetComponent<SpriteRenderer>().sprite;
        }
        if (enableReset)
        {
            currentFrame = 0; //total frame
            currentAnimFrame = 0;
            currentAnim = 0;
        }
    }
    // Update is called once per frame

    void Animate()
    {
        if (currentAnimFrame >= frames[currentAnim].frames)
        {
            frames[currentAnim].spriteObject.active = false;
            currentAnim += 1;
            if (frames.Length <= currentAnim)
            {
                if (loop)
                {
                    currentAnim = 0;
                }
                else
                {
                    if (next != null)
                    {
                        next.active = true;
                        currentAnim += -1;
                    }
                    else
                    {
                        if (dontDestroy == false)
                        {
                            Destroy(gameObject);
                        }
                        currentAnim += -1;
                    }
                }
            }
            else if (frames[currentAnim].spriteObject == null)
            {
                if (loop)
                {
                    currentAnim = 0;

                }
                else
                {
                    if (next != null)
                    {
                        next.active = true;
                        currentAnim += -1;
                    }
                    else
                    {
                        if (dontDestroy == false)
                        {
                            Destroy(gameObject);
                        }
                        currentAnim += -1;
                    }
                }
            }
            currentAnimFrame = 0;
        }
        if (currentAnimFrame == 0)
        {
            if (frames[currentAnim].objects.Length > 0)
            {
                foreach (GameObject g in frames[currentAnim].objects)
                {
                    GameObject dummy;
                    dummy = Instantiate(g, gameObject.transform.position + g.transform.position, Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
                    dummy.transform.localScale = gameObject.transform.localScale;
                }
            }
            if (frames[currentAnim].sfx.Length > 0)
            {
                for (int i = 0; i < frames[currentAnim].sfx.Length; i++)
                {
                    GameObject dummy;
                    dummy = Instantiate(sfxPrefab);
                    dummy.GetComponent<AudioSource>().Play();
                    dummy.GetComponent<sfxScript>().soundID = frames[currentAnim].sfx[i].soundID;
                    if (info != null)
                    {
                        dummy.GetComponent<sfxScript>().info = info;
                    }
                }
            }
        }
        frames[currentAnim].spriteObject.active = true;
    }


    void FixedUpdate()
    {
        BigDisable();
        if (info == null)
        {
            Animate();
            currentFrame += 1;
            currentAnimFrame += 1;
        }
        else
        {
            Vector3 dummyVect;
            if(info.hit < 0)
            {
                dummyVect = stop_destroy_otherOnHitArrayNEGATIVE[info.hit * -1];
                
            }
            else if(info.hit > 0)
            {
                dummyVect = stop_destroy_otherOnHitArray[info.hit];
            }
            else
            {
                dummyVect = new Vector3(0, 0, 0);
            }
            if(dummyVect.y == 1)
            {
                if (dummyVect.x == 1)
                {
                    killBasicAttack = true;
                }
                else
                {
                    gameObject.active = false;
                }
            }
            else
            {
                if(dummyVect.x == 0)
                {
                    Animate();
                    currentFrame += 1;
                    currentAnimFrame += 1;
                }
            }
            if(killBasicAttack && info.hit != 1)
            {
                gameObject.active = false;
            }
        }
    }
    void OnDisable()
    {
        GetComponent<SpriteRenderer>().sprite = null;
    }
}
