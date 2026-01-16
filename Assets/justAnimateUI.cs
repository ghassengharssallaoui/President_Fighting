using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class justAnimateUI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool loop; //
    public bool dontDestroy;
    public bool deactivateInstead;
    public GameObject next; //if null will go to idle in reference.
    public int currentFrame; //total frame
    public int currentAnimFrame;
    public int currentAnim;
    public anim[] frames;
    public GameObject sfxPrefab;
    public int STUPIDTIMER;
    public float updateTimer;
    /// <summary>
    /// 1 on x and y means that it's a "basic hit" ie. a hit that needs to be on screen only until the hit resolves
    /// </summary>
    bool killBasicAttack;
    public PlayerInfo infoscriptForFacing;
    void BigDeactivate() //is this even working?
    {

            for (int i = 0; i < frames.Length; i++)
            {
                if (frames[i] == null)
                {
                    break;
                }
                frames[i].spriteObject.active = false;
            }
        GetComponent<SpriteRenderer>().sprite = frames[0].spriteObject.GetComponent<SpriteRenderer>().sprite;
        gameObject.active = false;
        
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.AddComponent<networkSprite>();
        GetComponent<SpriteRenderer>().sprite = frames[0].spriteObject.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i] == null)
            {
                break;
            }
            frames[i].spriteObject.active = false;
        }
        //QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        //Application.targetFrameRate = 60;
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].spriteObject == null)
            {
                break;
            }
            frames[i].sprite = frames[i].spriteObject.GetComponent<SpriteRenderer>().sprite;
        }
        currentFrame = 0;
        currentAnimFrame = 0;
        currentAnim = 0;
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
                        if (dontDestroy == false)
                        {
                            Destroy(gameObject);
                        }
                        if (deactivateInstead == true)
                        {
                            BigDeactivate();
                            //you also need to set everything back to the begining Xander!!!!!
                        }
                    }
                    else
                    {
                        if (dontDestroy == false)
                        {
                            Destroy(gameObject);
                        }
                        if(deactivateInstead == true)
                        {
                            BigDeactivate();
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
                        if (dontDestroy == false)
                        {
                            Destroy(gameObject);
                        }
                        if (deactivateInstead == true)
                        {
                            BigDeactivate();
                        }
                    }
                    else
                    {
                        if (dontDestroy == false)
                        {
                            Destroy(gameObject);
                        }
                        if (deactivateInstead == true)
                        {
                            BigDeactivate();
                        }
                        currentAnim += -1;
                    }
                }
            }
            currentAnimFrame = 0;
        }
        if (currentAnimFrame == 0)
        {
            Vector3 dummyVect;
            if (frames[currentAnim].objects.Length > 0)
            {
                foreach (GameObject g in frames[currentAnim].objects)
                {
                    if (infoscriptForFacing == null)
                    {
                        dummyVect = gameObject.transform.position + g.transform.position;
                    }
                    else
                    {
                        dummyVect = new Vector3(gameObject.transform.position.x + g.transform.position.x * infoscriptForFacing.facing, gameObject.transform.position.y + g.transform.position.y, 0);
                    }
                    GameObject dummy;
                    dummy = Instantiate(g, dummyVect, Quaternion.identity); //will need to do the thing where we assign this the rotation and shit for these.
                    if (infoscriptForFacing == null)
                    {
                        dummy.transform.localScale = gameObject.transform.localScale;
                    }
                    else
                    {
                        dummy.transform.localScale = new Vector3(infoscriptForFacing.facing * dummy.transform.localScale.x, 1 * dummy.transform.localScale.y, 1);
                    }
                }
            }
            if (frames[currentAnim].sfx.Length > 0)
            {
                for (int i = 0; i < frames[currentAnim].sfx.Length; i++)
                {
                    GameObject dummy;
                    dummy = Instantiate(sfxPrefab);
                    dummy.GetComponent<AudioSource>().clip = frames[currentAnim].sfx[i].sfx;
                    dummy.GetComponent<AudioSource>().Play();
                }
            }
        }

        frames[currentAnim].spriteObject.active = true;
    }


    void FixedUpdate()
    {
        Animate();
        STUPIDTIMER += 1;
        currentFrame += 1;
        currentAnimFrame += 1;
    }
    void Update()
    {

        if (Time.timeScale == 0.00001f && Time.deltaTime < 0.00001f)
        {
            updateTimer += Time.deltaTime;
            if (updateTimer >= 0.00001f / 60f)
            {
                updateTimer += -0.00001f / 60f;
                FixedUpdate();

            }
        }

    }
}
