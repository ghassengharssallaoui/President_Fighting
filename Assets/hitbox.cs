using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour
{
    public bool p1collide;
    public bool p2collide;
    public bool p1default;
    public bool p2default;
    public float scaler;
    public string type;
    public string myName;
    string namestr;
    string namegb;
    hurtboxManager hm;
    Vector2 Pos1;
    Vector2 Pos2;
    public Vector2 topright;
    public Vector2 topleft;
    Vector2 bottomright;
    Vector2 bottomleft;
    Vector2 etopright;
    public Vector2 etopleft;
    Vector2 ebottomright;
    public Vector2 ebottomleft;
    Vector2 ydisplacement; //"Y" displacement, as in how much to displace from Pos1 and Pos2
    public hurtbox[] hurtboxesHit = new hurtbox[10];
    public bool setByHand;
    //public GameObject[] testObj;
    void OnEnable()
    {
        if(scaler == 0)
        {
            scaler = 1;
        }
        GameObject dummy;
        string dummyname;
        dummyname = "";
        dummy = gameObject;
        int failsafe;
        failsafe = 0;
        if (setByHand == false)
        {
            if (myName == "")
            {
                while ((dummy.GetComponent<PlayerInfo>() == null && dummy.GetComponent<hitBoxAssigner>() == null) && failsafe < 10)
                {
                    dummyname += dummy.gameObject.name;
                    dummy = dummy.transform.parent.gameObject;
                    failsafe += 1;
                }
                myName = dummyname;
                if (dummy.GetComponent<PlayerInfo>() != null)
                {
                    myName += "" + dummy.GetComponent<PlayerInfo>().player;
                }
                if(dummy.GetComponent<hitBoxAssigner>() != null)
                {
                    myName += "" + dummy.GetComponent<hitBoxAssigner>().player;
                }
            }
            else if (myName[myName.Length - 1] != '1' && myName[myName.Length - 1] != '2')
            {
                while ((dummy.GetComponent<PlayerInfo>() == null && dummy.GetComponent<hitBoxAssigner>() == null) && failsafe < 10)
                {
                    dummy = dummy.transform.parent.gameObject;
                    failsafe += 1;
                }
                if (dummy.GetComponent<PlayerInfo>() != null)
                {
                    myName += "" + dummy.GetComponent<PlayerInfo>().player;
                }
                if (dummy.GetComponent<hitBoxAssigner>() != null)
                {
                    myName += "" + dummy.GetComponent<hitBoxAssigner>().player;
                }
            }
            if (p1default == false && p2default == false)
            {
                if (dummy.GetComponent<PlayerInfo>() != null)
                {
                    if (dummy.GetComponent<PlayerInfo>().player == 1) //will need to change after we find out how many subparents deep we are
                    {
                        p2default = true;
                    }
                    else
                    {
                        p1default = true;
                    }
                }
                if (dummy.GetComponent<hitBoxAssigner>() != null)
                {
                    if (dummy.GetComponent<hitBoxAssigner>().player == 1) //will need to change after we find out how many subparents deep we are
                    {
                        p2default = true;
                    }
                    else
                    {
                        p1default = true;
                    }
                }
            }
        }
        if (hm == null)
        {
            hm = GameObject.Find("HurtboxManager").GetComponent<hurtboxManager>();
        }
        p2collide = p2default;
        p1collide = p1default;
    }
    void setMyCorners()
    {
        Pos1 = new Vector2(transform.position.x + Mathf.Cos(transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(transform.localScale.x * scaler) * 0.5f, transform.position.y + Mathf.Sin(transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(transform.localScale.x * scaler) * 0.5f);
        Pos2 = new Vector2(transform.position.x - Mathf.Cos(transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(transform.localScale.x * scaler) * 0.5f, transform.position.y - Mathf.Sin(transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(transform.localScale.x * scaler) * 0.5f);
        ydisplacement = new Vector2(0.5f * transform.localScale.y * scaler * Mathf.Cos((90 - transform.eulerAngles.z) * 0.0174533f), transform.localScale.y * scaler * Mathf.Sin((90 - transform.eulerAngles.z) * 0.0174533f) * 0.5f);
        topright = Pos1 + new Vector2(-ydisplacement.x, ydisplacement.y);
        bottomright = Pos1 + new Vector2(ydisplacement.x, -ydisplacement.y);
        topleft = Pos2 + new Vector2(-ydisplacement.x, ydisplacement.y);
        bottomleft = Pos2 + new Vector2(ydisplacement.x, -ydisplacement.y);
    }
    void setEnemyCorners(GameObject g)
    {
        Pos1 = new Vector2(g.transform.position.x + Mathf.Cos(g.transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(g.transform.localScale.x * scaler) * 0.5f, g.transform.position.y + Mathf.Sin(g.transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(g.transform.localScale.x * scaler) * 0.5f);
        Pos2 = new Vector2(g.transform.position.x - Mathf.Cos(g.transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(g.transform.localScale.x * scaler) * 0.5f, g.transform.position.y - Mathf.Sin(g.transform.eulerAngles.z * 0.0174533f) * Mathf.Abs(g.transform.localScale.x * scaler) * 0.5f);
        ydisplacement = new Vector2(0.5f * g.transform.localScale.y * scaler * Mathf.Cos((90 - g.transform.eulerAngles.z) * 0.0174533f), g.transform.localScale.y * scaler * Mathf.Sin((90 - g.transform.eulerAngles.z) * 0.0174533f) * 0.5f);
        etopright = Pos1 + new Vector2(-ydisplacement.x, ydisplacement.y);
        ebottomright = Pos1 + new Vector2(ydisplacement.x, -ydisplacement.y);
        etopleft = Pos2 + new Vector2(-ydisplacement.x, ydisplacement.y);
        ebottomleft = Pos2 + new Vector2(ydisplacement.x, -ydisplacement.y);
    }
    int checkCollision(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        float m1;
        float m2;
        float x;
        float y;
        m1 = (p1.y - p2.y) / ((p1.x - p2.x) + 0.001f);
        m2 = (p3.y - p4.y) / ((p3.x - p4.x) + 0.001f);
        if (m1 - m2 == 0)
        {
            m1 += 0.001f;
        }
        x = (p3.y - m2 * p3.x - p1.y + p1.x * m1) / (m1 - m2);
        y = m1 * (x - p1.x) + p1.y;
        //print(x);
        //print(y);
        if (
            ((x < p1.x + 0.01f && x > p2.x - 0.01f) || 
            (x < p2.x + 0.01f && x > p1.x - 0.01f)
            ) 
            && 
            (
            (x < p3.x + 0.01f && x > p4.x - 0.01f) ||
            (x < p4.x + 0.01f && x > p3.x - 0.01f
            ))
            &&
            ((y < p1.y + 0.01f && y > p2.y - 0.01f) ||
            (y < p2.y + 0.01f && y > p1.y - 0.01f)
            )
            &&
            (
            (y < p3.y + 0.01f && y > p4.y - 0.01f) ||
            (y < p4.y + 0.01f && y > p3.y - 0.01f
            ))
            )
        {

            return 1;
        }
        else
        {
            return 0;
        }
        print("just 1");
    }
    int finalCheck()
    {
        if(
            (topright.x < etopright.x && topright.y < etopright.y 
            && bottomright.x < ebottomright.x && bottomright.y > ebottomright.y 
            && topleft.x > etopleft.x && topleft.y < etopleft.y
            && bottomleft.x > ebottomleft.x && bottomleft.y > ebottomleft.y
            )
            ||
            (topright.x > etopright.x && topright.y > etopright.y 
            && bottomright.x > ebottomright.x && bottomright.y < ebottomright.y 
            && topleft.x < etopleft.x && topleft.y > etopleft.y
            && bottomleft.x < ebottomleft.x && bottomleft.y < ebottomleft.y)
            )

        {
            return 1;
        }else if (false)
        {
            return 1;
        }
        return 0;
    }
    bool fullCheck()
    {
        int checker = 0;
        checker += checkCollision(topright, topleft, etopleft, ebottomleft);
        checker += checkCollision(topright, topleft, etopleft, etopright);
        checker += checkCollision(topright, topleft, ebottomright, etopright);
        checker += checkCollision(topright, topleft, ebottomright, ebottomleft);


        //
        checker += checkCollision(topright, bottomright, etopleft, etopright);
        checker += checkCollision(topright, bottomright, etopleft, ebottomleft);
        checker += checkCollision(topright, bottomright, ebottomright, etopright);
        checker += checkCollision(topright, bottomright, ebottomright, ebottomleft);
        //
        checker += checkCollision(bottomleft, topleft, etopleft, etopright);
        checker += checkCollision(bottomleft, topleft, etopleft, ebottomleft);
        checker += checkCollision(bottomleft, topleft, ebottomright, etopright);
        checker += checkCollision(bottomleft, topleft, ebottomright, ebottomleft);
        //
        checker += checkCollision(bottomleft, bottomright, etopleft, etopright);
        checker += checkCollision(bottomleft, bottomright, etopleft, ebottomleft);
        checker += checkCollision(bottomleft, bottomright, ebottomright, etopright);
        checker += checkCollision(bottomleft, bottomright, ebottomright, ebottomleft);
        //
        checker += finalCheck();
        if (checker > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        setMyCorners();
        int counter;
        counter = 0;
        for (int i = 0; i < hurtboxesHit.Length; i++)
        {
            hurtboxesHit[i] = null;
        }
        if (p1collide)
        {
            
            foreach(GameObject g in hm.p1hurtboxes)
            {
                if (g != null) 
                {
                    setEnemyCorners(g);
                    if (fullCheck())
                    {
                        if(counter <= hurtboxesHit.Length -1)
                        {
                            hurtboxesHit[counter] = g.GetComponent<hurtbox>();
                            counter += 1;
                        }
                    }
                }
            }
          
        }
        if (p2collide)
        {
            foreach (GameObject g in hm.p2hurtboxes)
            {
                if (g != null)
                {
                    setEnemyCorners(g);
                    if (fullCheck())
                    {
                        if (counter <= hurtboxesHit.Length - 1)
                        {
                            hurtboxesHit[counter] = g.GetComponent<hurtbox>();
                            counter += 1;
                        }
                    }
                }
            }
        }
    }
    void OnDisable()
    {
        for (int i = 0; i < hurtboxesHit.Length; i++)
        {
            hurtboxesHit[i] = null;
        }
    }
}
