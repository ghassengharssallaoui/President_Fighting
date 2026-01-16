using UnityEngine;

public class GrappleTraj : MonoBehaviour
{
    PlayerInfo info;
    public float speed;
    public bool playerHit;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (info == null)
        {
            
            GameObject dummy;
            dummy = gameObject;
            while (dummy.GetComponent<PlayerInfo>() == null)
            {
                dummy = dummy.transform.parent.gameObject;
            }
            info = dummy.GetComponent<PlayerInfo>();
        }
        float x = info.GetComponent<grappleReference>().currentGrapple.GetComponent<TeddyGrappleMovement>().xSpeed;
        float y = info.GetComponent<grappleReference>().currentGrapple.GetComponent<TeddyGrappleMovement>().ySpeed;
        if(y + info.transform.position.y <= 0)
        {
            if(x > 0 && transform.position.x > info.GetComponent<grappleReference>().currentGrapple.transform.position.x)
            {

                y = -10;
                if (playerHit == false)
                {
                    x = 0;
                }
                info.transform.position += new Vector3(0, -10, 0);

            }
            else if (x < 0 && transform.position.x < info.GetComponent<grappleReference>().currentGrapple.transform.position.x)
            {
                y = -10; 
                if(playerHit == false)
                {
                    x = 0;
                } 
                info.transform.position += new Vector3(0, -10, 0);
            }
            else
            {
                y = 0f;
                
            }
        }
        info.traj = new Vector3(x * speed, y * speed, 0);

    }

}
