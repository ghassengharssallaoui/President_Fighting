using UnityEngine;

public class grappleHitBoxInterpreter : MonoBehaviour
{
    PlayerInfo info;
    hitbox hbox;
    void Start()
    {

        hbox = GetComponent<hitbox>();
        info = transform.parent.GetComponent<TeddyGrappleMovement>().info;
        if(info.player == 1)
        {
            hbox.p2default = true;
            hbox.p2collide = true;
        }
        else
        {
            hbox.p1default = true;
            hbox.p1collide = true;
        }
    }
    void Counter()
    {
        info.hit = 1;
        info.enemyObject.GetComponent<PlayerInfo>().hit = 2;
    }
    void WeakHit()
    {
        info.enemyObject.GetComponent<PlayerInfo>().hit = -4;
        info.enemyObject.GetComponent<PlayerInfo>().traj = new Vector3(0,0,0);
        info.enemyObject.GetComponent<PlayerInfo>().currentAnim.active = false;
        info.enemyObject.GetComponent<PlayerInfo>().grabStun.active = true;
        transform.parent.GetComponent<TeddyGrappleMovement>().enabled = false;
        info.currentAnim.active = false;
        info.GetComponent<grappleReference>().playerGrapple.active = true;
        hbox.p1collide = false;
        hbox.p2collide = false;
        hbox.p1default = false;
        hbox.p2default = false;
    }
    // Update is called once per frame
    void Update()
    {
        foreach (hurtbox h in hbox.hurtboxesHit)
        {
            bool doit;
            doit = true;
            if (h != null)
            {
                if (doit == true)
                {

                    if (h.player == 3)//hit a third party.
                    {
                        if (h.type == "Projectile")
                        {
                            //Projectile Stuff
                        }
                    }
                    else if (h.player != info.player) //hit the opponent
                    {
                        if (h.type == "Counter")
                        {
                            Counter();
                        }
                        else //default or unknown
                        {
                             WeakHit();
                        }
                    }
                    else //hit yourself
                    {

                    }
                }
            }
        }
    }
}
