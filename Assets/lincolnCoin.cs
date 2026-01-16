using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lincolnCoin : MonoBehaviour
{
    PlayerInfo info;
    public float range;
    public GameObject camTransform;
    public Vector3 startingPosition;
    public Vector3 trueStartPosition;
    public Vector3 finalPosition;
    public float fallSpeed;
    public float totalTime;
    public float maxTime;
     float D;
     float E;
     float F;
     float G;
     float H;
     float I;
     float J;
     float K;
     float L;
     float M;
     float A;
     float V;
     float counter;
     float xPos;
    public SpriteRenderer deadSprite;
    public SpriteRenderer fallSprite;
    public GameObject teleportAnimation;
    public bool destroyNextFrame;
    public GameObject reticle;
    public GameObject reticleFinder;
    public GameObject teleportHitbox;
    public bool canceled;
    // Start is called before the first frame update

    void Start()
    {
        trueStartPosition = transform.position;
        info = GetComponent<findPlayer>().player;
        startingPosition = transform.position;
        //Vector3 playerPosition = info.transform.position + new Vector3(0, 2.47f, 0);
        float ylimit;
        ylimit = info.receiver.moveVector.y;
        if(info.receiver.moveVector.y > 0.5f)
        {
            ylimit = 0.5f;
        }
        if (info.receiver.moveVector.y < -0.5f)
        {
            ylimit = -0.5f;
        }
        finalPosition = startingPosition + new Vector3(info.receiver.moveVector.x * range +0.001f, ylimit * range, 0);
        if(finalPosition.y < 2.46f)
        {
            finalPosition = new Vector3(finalPosition.x, 2.47f, 0);
        }
        if(finalPosition.x > 35)
        {
            finalPosition = new Vector3(35, finalPosition.y, 0);

        }
        if (finalPosition.x < -35)
        {
            finalPosition = new Vector3(-35, finalPosition.y, 0);

        }
        if(Mathf.Abs(info.receiver.moveVector.y) < 0.15f && Mathf.Abs(info.receiver.moveVector.x) < 0.15f)
        {
            finalPosition = new Vector3(info.enemyScript.transform.position.x, info.enemyScript.transform.position.y + 2.47f, 0);
        }
        D = startingPosition.x;
        E = startingPosition.y;
        F = finalPosition.x;
        G = finalPosition.y;
        J = fallSpeed;
        K = totalTime;
        A = (G - E - J * K * K) / K; //vertical speed;
        L = -A / (J * 2);//peak timing;
        I = E + A * L + J * L * L; // vertice height;
        M = (F - D) / K;
        H = D + M * L;
        V = (E - I) / ((D - H) * (D - H));
        xPos = trueStartPosition.x;
        AddToCam();
        GameObject dummy = Instantiate(reticle, finalPosition, Quaternion.identity);
        reticleFinder.GetComponent<followOffScreenObject>().followObject = dummy;
        teleportHitbox.GetComponent<HitBoxInterpreter>().info = info;
        if (info.player == 1)
        {
            teleportHitbox.GetComponent<hitbox>().p1default = false;
            teleportHitbox.GetComponent<hitbox>().p2default = true;
        }
        else
        {
            teleportHitbox.GetComponent<hitbox>().p2default = false;
            teleportHitbox.GetComponent<hitbox>().p1default = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(info.hit != 0)
        {
            Destroy(gameObject);
        }
        float x;
        x = trueStartPosition.x + M * counter;
        float y;
        y = V * (x - H) * (x - H) + I;
        if(x > 35)
        {
            x = 35;
        }
        if(x < -35)
        {
            x = -35;
        }
        if (info.receiver.holdingMovement == 2 && info.cpuLevel == 0)
        {
            canceled = true;
            deadSprite.enabled = false;
            fallSprite.enabled = true;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<AudioSource>().Stop();
        }
        transform.position = new Vector3(x, y, 0);
        counter += 1;
        if (destroyNextFrame)
        {
            Destroy(gameObject);
        }
        if(counter >= totalTime)
        {
            if(counter == totalTime)
            {
                deadSprite.enabled = true;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (counter > maxTime)
            {
                RemoveFromCam();
                camTransform.active = false;
                deadSprite.enabled = false;
                fallSprite.enabled = true;
            }
            else if (!canceled)
            {
                teleportAnimation.active = true;
                if(info.currentAnim.name != "SuperStartUp (1)")
                {
                    teleportHitbox.active = true;
                }
                Vector3 oldPos = info.transform.position;
                float trueY;
                trueY = transform.position.y + -2.5f;
                if(trueY < 1)
                {
                    trueY = 0;
                }
                info.transform.position = new Vector3(transform.position.x, trueY, 0);
                info.GetComponent<lincolnTeleportAnimationHandler>().teleport = true;
                destroyNextFrame = true;
            }
            
            ///do the teleport
        }
        if (transform.position.y <= 0)
        {
            Destroy(gameObject);
        }
        string name;
        name = info.currentAnim.name;
        if(name == "FallFlash")
        {
            Destroy(gameObject);
        }
    }
    void AddToCam()
    {
        for (int i = 0; i < info.cam.otherOnScreenObjects.Length; i++)
        {
            if (info.cam.otherOnScreenObjects[i] == null)
            {
                info.cam.otherOnScreenObjects[i] = camTransform;
                i = 10000;
            }
        }
    }
    void RemoveFromCam()
    {
        for (int i = 0; i < info.cam.otherOnScreenObjects.Length; i++)
        {
            if (info.cam.otherOnScreenObjects[i] != null)
            {
                if (info.cam.otherOnScreenObjects[i] == camTransform)
                {
                    info.cam.otherOnScreenObjects[i] = null;
                }
            }
        }
    }
    void OnDestroy()
    {
        RemoveFromCam();
    }
}
