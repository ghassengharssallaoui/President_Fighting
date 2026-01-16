using UnityEngine;

public class grappleReference : MonoBehaviour
{
    public GameObject playerGrapple;
    public GameObject airGrapple;
    public GameObject currentGrapple;
    public bool wallBounce;
    public bool groundSlam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerInfo>().hit != 0)
        {
            Destroy(currentGrapple);
        }
        bool doit;
        if (transform.position.y == 0 || GetComponent<PlayerInfo>().hit != 0) 
        { 
            wallBounce = false;
            groundSlam = false;
        }
    }
}
