using UnityEngine;

public class grappleWallBounce : MonoBehaviour
{
    PlayerInfo info;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(info.transform.position.x + info.traj.x) >= 35)
        {
            info.GetComponent<OptionsReference>().airDefault.active = true;
            Destroy(info.GetComponent<grappleReference>().currentGrapple);
            info.traj = new Vector3(info.traj.x * -0.4f, info.traj.y * 0.3f, 0);
            gameObject.active = false;
        }
        if(info.transform.position.y + info.traj.y <= 0)
        {
            Destroy(info.GetComponent<grappleReference>().currentGrapple);
        }
    }
}
