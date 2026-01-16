using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperFlashBackground : MonoBehaviour
{
    PlayerInfo infoScript;
    public bool usePositionOverFacing;
    float targetx;
    float scale;
    public GameObject locator;
    public float updateTimer;
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
        scale = transform.localScale.y;
        if (usePositionOverFacing)
        {
            if (infoScript.transform.position.x < infoScript.enemyScript.transform.position.x)
            {
                if (infoScript.transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(scale, scale, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-scale, scale, 1);
                }
                locator = GameObject.Find("P1SuperFlashLocator");
                transform.position = locator.transform.position;
                targetx = transform.position.x + 12.1f;
            }
            else
            {
                if (infoScript.transform.localScale.x == -1)
                {
                    transform.localScale = new Vector3(scale, scale, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-scale, scale, 1);
                }
                locator = GameObject.Find("P2SuperFlashLocator");
                transform.position = locator.transform.position;
                targetx = transform.position.x - 12.1f;
            }
        }
        else
        {
            if (infoScript.facing == 1)
            {
                if (infoScript.transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(scale, scale, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-scale, scale, 1);
                }
                locator = GameObject.Find("P1SuperFlashLocator");
                transform.position = locator.transform.position;
                targetx = transform.position.x + 12.1f;
            }
            else
            {
                if (infoScript.transform.localScale.x == -1)
                {
                    transform.localScale = new Vector3(scale, scale, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-scale, scale, 1);
                }
                locator = GameObject.Find("P2SuperFlashLocator");
                transform.position = locator.transform.position;
                targetx = transform.position.x - 12.1f;
            }
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public float counter;
    void FixedUpdate()
    {
        counter = (targetx - transform.position.x) / 3;
        transform.position = new Vector3(transform.position.x + counter, locator.transform.position.y, locator.transform.position.z);
        if(Time.timeScale != 0.00001f)
        {
            print("killed");
            gameObject.active = false;
        }
    }
    void Update()
    {

            if (Time.timeScale <= 0.00001f && Time.deltaTime < 0.00001f)
            {
            //print(Time.deltaTime);
            updateTimer += Time.deltaTime;
            if (updateTimer >= 0.00001f / 60f)
                {
                updateTimer += -0.00001f / 60f;
                FixedUpdate();
                }
            }
        
    }
    void OnDisable()
    {
        counter = 0;
        transform.position = new Vector3(0, 1000, 0);
    }
}
