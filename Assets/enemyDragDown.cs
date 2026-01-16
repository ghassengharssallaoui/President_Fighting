using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDragDown : MonoBehaviour
{
    public PlayerInfo enemy;
    public int facing;
    bool rotated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {


            if (enemy.currentAnim != enemy.tumble)
            {
                enemy.currentAnim.active = false;
                enemy.tumble.active = true;
            }

            enemy.hit = -1;
            enemy.facing = -facing;
            enemy.traj = new Vector3(0, 0, 0);
            if (rotated == false)
            {
                enemy.tumble.GetComponent<rotateByTraj>().enabled = false;
                rotated = true;
            }
            enemy.transform.eulerAngles = new Vector3(0, 0, -45 * facing);
            enemy.transform.position = transform.position;

        }
    }
    void OnDisable()
    {
        enemy.transform.position = new Vector3(enemy.transform.position.x, 0, 0);
        enemy.tumble.active = false;
        enemy.health = -1;
        enemy.transform.eulerAngles = new Vector3(0, 0, 0);

        if (enemy.nonFatal)
        {
            enemy.regularDeath.active = true;
        }
        else
        {
            enemy.dustFatality.active = true;
            enemy.enemyScript.currentAnim.active = false;
            enemy.enemyScript.victoryAnim.active = true;
            enemy.hit = 0;
        }
    }
}
