using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterCameraMovement : MonoBehaviour
{
    public GameObject[] points = new GameObject[4];
    public float minZoom;
    public float leftBound; //traditional up down left right.
    public float rightBound;
    public float topBound;
    public float bottomBound;
    public float xyRatio;
    public float realityRatio;
    public GameObject p1;
    public GameObject p2;
    bool biggerX; //is true when X is the bigger of the two.
    int superTimer;
    public Vector3[] dummyVector = new Vector3[4];
    public Vector3 vectie;
    public bool ignoreBounds;
    public bool ignoreZoom;
    int snapOnLoad;
    public bool pauseOnPause;
    public float aspectMultiplier;
    [ HideInInspector] public float defaultZoom;
    float updateTimer;
    public GameObject[] otherOnScreenObjects = new GameObject[10];
    // Start is called before the first frame update
    void RestoreDefaults()
    {
        ignoreBounds = false;
        ignoreZoom = false;
        pauseOnPause = false;
    }
    void Start()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == null)
            {
                dummyVector[i] = new Vector3(0, 0, 0);
            }
            else
            {
                dummyVector[i] = points[i].transform.position;
            }
        }
        defaultZoom = minZoom;
        //RestrictPoints();
        snapOnLoad = 0;
    }
    float calculateAspect()
    {
        float screenheight = Screen.height;
        float screenwidth = Screen.width;
        float bastardRes = screenwidth / screenheight;//bastard because i'm pissed I even have to do this
        xyRatio = 1 / bastardRes;
        return 1.7777f / bastardRes;
    }
    Vector3 CalculateZoom(Vector3[] points)
    {
        aspectMultiplier = calculateAspect();
        float topy = -1000;
        float bottomy = 1000;
        float leftx = 1000;
        float rightx = -1000;
        float zoom;
        float xpos;
        float ypos;
        foreach (Vector3 g in points)
        {
            if (g.x > rightx)
            {
                rightx = g.x;
            }
            if (g.x < leftx)
            {
                leftx = g.x;
            }
            if (g.y > topy)
            {
                topy = g.y;
            }
            if (g.y < bottomy)
            {
                bottomy = g.y;
            }
        }
        if ((Mathf.Abs(rightx - leftx) > Mathf.Abs(topy - bottomy) / xyRatio) || Mathf.Abs(rightx - leftx) > rightBound - leftBound - 1)
        {
            biggerX = true;
            zoom = aspectMultiplier * Mathf.Abs(rightx - leftx) * 1 / 2;
        }
        else
        {
            biggerX = false;
            zoom = aspectMultiplier* Mathf.Abs(topy - bottomy) * realityRatio * 0.5f / xyRatio;
        }
        if (zoom < minZoom)
        {
            if (ignoreBounds == false && ignoreZoom == false)
            {
                zoom = minZoom;
            }
        }
        xpos = (leftx + rightx) / 2;
        ypos = (bottomy);
        if (ignoreBounds == false)
        {
            if (bottomy - zoom * xyRatio * realityRatio < bottomBound)
            {
                ypos = bottomBound + zoom * xyRatio * realityRatio / aspectMultiplier;
            }
            else if (ypos + zoom * xyRatio * realityRatio > topBound)
            {
                ypos = topBound - zoom * xyRatio * realityRatio / aspectMultiplier;
            }
            if (xpos - zoom * realityRatio / aspectMultiplier < leftBound )
            {
                xpos = leftBound + zoom * realityRatio / aspectMultiplier;
            }
            else if (xpos + zoom * realityRatio / aspectMultiplier > rightBound )
            {
                xpos = rightBound - zoom * realityRatio / aspectMultiplier;
            }
        }
        return new Vector3(xpos, ypos, -zoom);
    }
    void RestrictPoints()
    {
        for(int i = 0; i < dummyVector.Length; i++)
        {
            if (dummyVector[i].x < leftBound)
            {
                dummyVector[i] = new Vector3(leftBound, dummyVector[i].y, 0);
            }
            if (dummyVector[i].x > rightBound)
            {
                dummyVector[i] = new Vector3(rightBound, dummyVector[i].y, 0);
            }
            if (dummyVector[i].y < bottomBound)
            {
                dummyVector[i] = new Vector3(dummyVector[i].x, bottomBound, 0);
            }
            if (dummyVector[i].y > topBound)
            {
                dummyVector[i] = new Vector3(dummyVector[i].x, topBound, 0);
            }
        }
    }
    void doubleCheckZoomAndBounds()
    {
        OptionsReference p1info = p1.GetComponent<OptionsReference>();
        OptionsReference p2info = p2.GetComponent<OptionsReference>();
        if ((p1info.airDefault.active == true || p1info.groundDefault.active == true) &&(p2info.airDefault.active == true || p2info.groundDefault.active == true))
        {
            ignoreBounds = false;
            ignoreZoom = false;
            minZoom = defaultZoom;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        doubleCheckZoomAndBounds();
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == null)
            {
                dummyVector[i] = new Vector3(0, 0, 0);
            }
            else
            {
                dummyVector[i] = points[i].transform.position;
            }
        }

        int dummyInt = 0;
        for (int i = 0; i < otherOnScreenObjects.Length; i++)
        {
            if (otherOnScreenObjects[i] != null)
            {
                dummyInt += 1;
            }
        }
        dummyInt += 4;
        Vector3[] superDummyVect = new Vector3[dummyInt];
        superDummyVect[0] = dummyVector[0];
        superDummyVect[1] = dummyVector[1];
        superDummyVect[2] = dummyVector[2];
        superDummyVect[3] = dummyVector[3];
        int dummyVectInt = 4;
        for (int i = 0; i < otherOnScreenObjects.Length; i++)
        {
            if (otherOnScreenObjects[i] != null)
            {
                Vector3 boundedObjectVector;
                boundedObjectVector = otherOnScreenObjects[i].transform.position;
                if(boundedObjectVector.x < leftBound)
                {
                    boundedObjectVector = new Vector3(leftBound, boundedObjectVector.y, 0);
                }
                if (boundedObjectVector.x > rightBound)
                {
                    boundedObjectVector = new Vector3(rightBound, boundedObjectVector.y, 0);
                }
                if (boundedObjectVector.y > topBound)
                {
                    boundedObjectVector = new Vector3(boundedObjectVector.x, topBound, 0);
                }
                if (boundedObjectVector.y < bottomBound)
                {
                    boundedObjectVector = new Vector3(boundedObjectVector.x, bottomBound, 0);
                }
                superDummyVect[dummyVectInt] = boundedObjectVector;
                dummyVectInt += 1;
            }
        }
        vectie = CalculateZoom(superDummyVect);
        transform.position = vectie;

    }
    void Update()
    {
        if(p1.GetComponent<PlayerInfo>().health == 12 && p2.GetComponent<PlayerInfo>().health == 12 && p1.GetComponent<PlayerInfo>().hit == 0 && p1.GetComponent<PlayerInfo>().hit == 0)
        {
            RestoreDefaults();
        }
        if(Time.timeScale == 0.00001f && pauseOnPause == false && Time.deltaTime < 0.0001f)
        {
            updateTimer += Time.deltaTime;
            if (Time.timeScale <= 0.00001f)
            {
                if (updateTimer >= 0.00001f / 60f)
                {
                    updateTimer += -0.00001f / 60f;
                    FixedUpdate();
                }
            }
        }
        if (snapOnLoad == 2)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == null)
                {
                    dummyVector[i] = new Vector3(0, 0, 0);
                }
                else
                {
                    dummyVector[i] = points[i].transform.position;
                }
            }
            //RestrictPoints();
            vectie = CalculateZoom(dummyVector);
            transform.position = vectie;
            if (p1 != null && p2 != null)
            {
                vectie = CalculateZoom(dummyVector);
                transform.position = vectie;
                

            }
        }
        snapOnLoad += 1;
    }
    void ClampPosition()
    {
        float x = vectie.x * 10;
        float y = vectie.y * 10;
        y = Mathf.Round(y) / 10f;
        x = Mathf.Round(x) / 10f;
        print(x);
        vectie = new Vector3(x, 10, vectie.z);
    }
}
