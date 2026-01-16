using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxInterpreter : MonoBehaviour
{
    [HideInInspector] public hitbox hbox;
    [HideInInspector] public PlayerInfo info;
    public bool strongHit = true;
    public bool weakHit;
    public int hitID; //2 is super, 4 is other.
    public int stunFrames;
    public int knockbackFrames;
    public Vector3 knockbackAngle;
    public bool ignoreCounter;
    public bool setByHand;
    public bool destroyOnHit;
    public string hitSound;
    public int destroyProjectilesOfIDEqualToOrAboveThisValue;
    HitBoxSoundGenerator soundGen;
    GameObject godObject;
    // Start is called before the first frame update
    void Start()
    {
        soundGen = GameObject.Find("HitBoxSoundGenerator").GetComponent<HitBoxSoundGenerator>();
        if (!setByHand)
        {
            hbox = GetComponent<hitbox>();
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null && dummy.GetComponent<hitBoxAssigner>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            if (dummy.GetComponent<PlayerInfo>() != null)
            {
                info = dummy.GetComponent<PlayerInfo>();
            }
            if (dummy.GetComponent<hitBoxAssigner>() != null)
            {
                info = dummy.GetComponent<hitBoxAssigner>().info;
            }
            godObject = dummy;
        }
    }
    void StrongHit()
    {
        info.hit = 1;
        info.enemyObject.GetComponent<PlayerInfo>().hit = -1;
    }
    void Other()
    {
        info.hit = hitID;
        info.enemyObject.GetComponent<PlayerInfo>().hit = -hitID;
        if (hitID == 2)
        {
            info.superTimer = 0;
            info.enemyObject.GetComponent<PlayerInfo>().superTimer = 0;
        }
        if (hitID == 4)
        {
            info.otherTimer = 0;
            info.enemyObject.GetComponent<PlayerInfo>().otherTimer = 0;
        }
        if (hitID == 5)
        {
            info.otherTimer2 = 0;
            info.enemyObject.GetComponent<PlayerInfo>().otherTimer2 = 0;
        }
    }
    void WeakHit()
    {
        info.enemyObject.GetComponent<PlayerInfo>().hit = -3;
        info.enemyObject.GetComponent<PlayerInfo>().currentAnim.active = false;
        info.enemyObject.GetComponent<PlayerInfo>().traj = new Vector3(0.1f, 0, 0);
        GameObject h = info.enemyObject.GetComponent<OptionsReference>().hitDefault;
        h.active = true;
        basicHit b = h.GetComponent<basicHit>();
        b.knockbackAngle = knockbackAngle;
        b.knockbackPoint = -knockbackFrames;
        b.hitTimer = -knockbackFrames - stunFrames;
    }
    void Counter()
    {
        info.hit = 1;
        info.enemyObject.GetComponent<PlayerInfo>().hit = 2;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (hurtbox h in hbox.hurtboxesHit)
        {
            bool doit;
            doit = true;
            if (h != null)
            {
                if (h.projectileImmune && hbox.type == "Projectile")
                {
                    doit = false;
                }
                if (doit == true)
                {

                    if (h.player == 3)//hit a third party.
                    {
                        if (h.type == "Projectile")
                        {
                            if (destroyProjectilesOfIDEqualToOrAboveThisValue >= h.typeID)
                            {
                                Destroy(h.transform.root.gameObject);
                                if (h.destroyObject != null && info.hit >= 0)
                                {
                                    Instantiate(h.destroyObject, h.transform.root.transform.position, Quaternion.identity);
                                }
                            }
                        }
                    }
                    else if (h.player != info.player) //hit the opponent
                    {
                        soundGen.soundID = hitSound;
                        if (h.type == "Counter" && ignoreCounter != true)
                        {
                            Counter();
                        }
                        else //default or unknown
                        {
                            if (strongHit)
                            {
                                StrongHit();
                            }
                            if (weakHit)
                            {
                                if (info.enemyObject.GetComponent<PlayerInfo>().hit != -3)
                                {
                                    WeakHit();
                                }
                                else
                                {
                                    StrongHit();
                                }
                            }
                            if (hitID != 0 && hitID != 1)
                            {
                                Other();
                            }

                        }
                        if (destroyOnHit)
                        {
                            godObject.active = false;
                        }
                    }
                    else //hit yourself
                    {

                    }
                }
            }
        }
    }
}
