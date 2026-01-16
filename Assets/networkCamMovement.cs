using UnityEngine;

public class networkCamMovement : MonoBehaviour
{
    public Vector3[] dummyVector = new Vector3[4];
    public Vector3 starterVector;
    public float xyRatio;
    public float leftBound = -35; //traditional up down left right.
    public float rightBound = 35;
    public float topBound = 39;
    public float bottomBound = -1.6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        bool biggerX;
        float aspectMultiplier = calculateAspect();
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
            zoom = aspectMultiplier * Mathf.Abs(topy - bottomy) * 1 * 0.5f / xyRatio;
        }
        xpos = (leftx + rightx) / 2;
        ypos = (bottomy);
        if (bottomy - zoom * xyRatio * 1 < bottomBound)
        {
            ypos = bottomBound + zoom * xyRatio * 1 / aspectMultiplier;
        }
        else if (ypos + zoom * xyRatio * 1 > topBound)
        {
            ypos = topBound - zoom * xyRatio * 1 / aspectMultiplier;
        }
        if (xpos - zoom * 1 / aspectMultiplier < leftBound)
        {
            xpos = leftBound + zoom * 1 / aspectMultiplier;
        }
        else if (xpos + zoom * 1 / aspectMultiplier > rightBound)
        {
            xpos = rightBound - zoom * 1 / aspectMultiplier;
        }
        return new Vector3(xpos, ypos, -zoom);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        float zoom = -starterVector.z;
        dummyVector[0] = new Vector3(starterVector.x + zoom * 1.025f, starterVector.y + zoom * 0.575f, 0);
        dummyVector[1] = new Vector3(starterVector.x - zoom * 1.025f, starterVector.y + zoom * 0.575f, 0);
        dummyVector[2] = new Vector3(starterVector.x + zoom * 1.025f, starterVector.y - zoom * 0.575f, 0);
        dummyVector[3] = new Vector3(starterVector.x - zoom * 1.025f, starterVector.y - zoom * 0.575f, 0);

        transform.position = CalculateZoom(dummyVector);
    }
}
