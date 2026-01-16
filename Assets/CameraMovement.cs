using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 p1pos;
    public Vector3 p2pos;
    public float ybottom;
    public float xMax;
    public float yMax;
    public float headroom = 8;
    public float zoom;
    public float zoomAccel;
    public float zoomSpeed;
    public float zoomCounter;
    public float lastzoom;
    public Vector3 target;
    void CalculateZoom(float x1, float x2, float y1, float y2, float dummy)
    {
        float xmax;
        float xmin;
        float ymax;
        float ymin;
        if(x1 > x2)
        {
            xmax = x1 + dummy;
            xmin = x2 - dummy;
        }
        else
        {
            xmax = x2 + dummy;
            xmin = x1 - dummy;
        }
        if (y1 > y2)
        {
            ymax = y1;
            ymin = y2 - headroom;
        }
        else
        {
            ymax = y2;
            ymin = y1 - headroom;
        }
        if(xmin < -xMax)
        {
            xmin = -xMax;
        }
        if (xmax > xMax)
        {
            xmax = xMax;
        }
        if(ymin < ybottom)
        {
            ymin = ybottom;//add more code here if we want to zoom into the foreground;
        }
        if(ymax > yMax)
        {
            ymax = yMax;
        }
        if((xmax-xmin) * 0.578f > (ymax-ymin) * 0.77f)//if the length is greater than the width
        {
            zoom = (xmax - xmin) * 0.77f;
        }
        else
        {
            zoom = (ymax - ymin) ;
        }
        if (zoom < 20)
        {
            zoom = 20;
        }
    }
    void AdjustCam(float z)
    {
        if (z * 0.77f + target.x > xMax)
        {
            target = new Vector3(xMax - z * 0.77f, target.y, 0);
        }
        if (-z * 0.77f + target.x < -xMax)
        {
            target = new Vector3(-xMax + z * 0.77f, target.y, 0);
        }
        if (z * 0.578f + target.y > yMax)
        {
            target = new Vector3(target.x, yMax - z * 0.578f, 0);
        }
        if (-z * 0.578f + target.y < ybottom)
        {
            target = new Vector3(target.x, ybottom + z * 0.578f, 0);
        }
    }
    //
    // Up
    // date is called once per frame
    void Update()
    {
        float dummynum;
        dummynum = Mathf.Abs(p1pos.x - p2pos.x)/ 12;
        target = new Vector3((p1pos.x + p2pos.x) / 2, (p1pos.y + p2pos.y) / 2, 0);

        CalculateZoom(p1pos.x, p2pos.x, p1pos.y + headroom, p2pos.y + headroom, dummynum);
        if (Random.Range(0, 1000) == 0)
        {
            print(zoom);
            print(lastzoom);
        }
        if (zoom >= lastzoom)
        {
            AdjustCam(zoom);
            transform.position = new Vector3(target.x, target.y, -zoom);
            zoomCounter = 0;
            zoomAccel = 0;
            zoomSpeed = 0;
        }
        else
        {
            print("it's trying");
            float thiszoom;
            if (zoomSpeed > (-transform.position.z - zoom) / 2)
            {
                zoomCounter = 1000;

            }
            if (zoomCounter < 5)
            {
                zoomAccel = (-transform.position.z - zoom) / 100;
                zoomSpeed += zoomAccel;
            }
            else if (zoomCounter < 1000)
            {
                zoomAccel = (-transform.position.z - zoom) * zoomCounter / 500;
                zoomSpeed += zoomAccel;
            }
            else
            {
                if (zoomSpeed < (-transform.position.z - zoom) / 2)
                {
                    zoomCounter = 0;
                }
                else
                {
                    zoomSpeed = (-transform.position.z - zoom) / 2;
                }
            }
            thiszoom = transform.position.z - (zoomSpeed * Time.deltaTime);
            AdjustCam(thiszoom);
            transform.position = new Vector3(target.x, target.y, thiszoom);
        }
        zoomCounter += Time.deltaTime * 60;
        lastzoom = -transform.position.z;

    }

}
