using UnityEngine;

public class TeddyGrappleMovement : MonoBehaviour
{
    public PlayerInfo info;
    public Vector3 startingPosition;
    public float xSpeed;
    public float ySpeed;
    public float magnitude;
    public int extendFrames;
    public int waitFrames;
    public float snapBackFrames;
    float counter;
    float counter2;
    float counter3;
    Vector3 positionDelta;
    public GameObject hitbox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) >= 35 || transform.position.y < 0)
        {
            if(transform.position.x > 35)
            {
                transform.position = new Vector3(35, transform.position.y, 0);
            }
            if (transform.position.x < -35)
            {
                transform.position = new Vector3(-35, transform.position.y, 0);
            }
            if(transform.position.y < 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);

            }
            extendFrames = 0;
            if (info.GetComponent<grappleReference>().wallBounce == false && Mathf.Abs(transform.position.x -0.14f) >= 35)
            {
                waitFrames = 100000;
                info.GetComponent<grappleReference>().wallBounce = true;
                info.currentAnim.active = false;
                info.GetComponent<grappleReference>().airGrapple.active = true;
                hitbox.active = false;
                info.traj = new Vector3(0, 0, 0);

            }
            if (info.GetComponent<grappleReference>().groundSlam == false && transform.position.y <= 3.54f)
            {
                waitFrames = 100000;
                info.GetComponent<grappleReference>().groundSlam = true;
                info.currentAnim.active = false;
                info.GetComponent<grappleReference>().airGrapple.active = true;
                hitbox.active = false;
                GetComponent<TeddyGrappleMovement>().enabled = false;
            }
        }
        if (transform.position.z == 0) //this is leftover and I don't know if I need it. Just imagine this as always being true;
        {
            if (counter <= extendFrames)
            {
                transform.position = startingPosition + info.transform.position + new Vector3(xSpeed * magnitude * counter, ySpeed * magnitude * counter, 0);
                magnitude = magnitude * .975f;
                counter += 1;
            }
            else
            {
                if (counter2 < waitFrames)
                {
                    transform.position = startingPosition + info.transform.position + new Vector3(xSpeed * magnitude * counter, ySpeed * magnitude * counter, 0);
                    counter2 += 1;
                }
                else { 
                    if (counter3 < snapBackFrames)
                    {
                        hitbox.active = false;
                        if (counter3 == 0)
                        {
                             positionDelta = transform.position - (startingPosition + info.transform.position);
                        }
                        float percentage = 1f / snapBackFrames;
                        transform.position = startingPosition + info.transform.position + positionDelta - new Vector3(positionDelta.x * (percentage * counter3), positionDelta.y * (percentage * counter3), 0);
                        counter3 += 1;
                    }
                    else
                    {
                        if(info.hit == 0)
                        {
                            info.currentAnim.active = false;
                            info.transform.position += new Vector3(0, 0.0001f, 0);
                            info.GetComponent<OptionsReference>().airDefault.active = true;
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            xSpeed = Mathf.Cos(transform.eulerAngles.z * 0.0174533f);
            ySpeed = Mathf.Sin(transform.eulerAngles.z * 0.0174533f);
            startingPosition = transform.position - info.transform.position;
            info.GetComponent<grappleReference>().currentGrapple = gameObject;
        }
    }
}
