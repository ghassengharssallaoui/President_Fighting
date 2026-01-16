using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObamaChargeUI : MonoBehaviour
{
    public int totalChargeTime;
    public int totalAnimationTime;
    public JustAnimate shot1;
    public int charge;
    public PlayerInfo info;
    public GameObject groundOption;
    public GameObject airOption;
    public bool notObama;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);

        charge = totalChargeTime;
        if (info.player == 1)
        {
            GetComponent<followExactly>().target = GameObject.Find("ObamaUIPlayer1");
        }
        if (info.player == 2)
        {
            GetComponent<followExactly>().target = GameObject.Find("ObamaUIPlayer2");
        }
    }
    void Animate() { //Should be called (reset animation should the charge reach a certain point
        if ((groundOption.active == true || airOption.active == true) && charge > 120)
        {
            charge = 0;
        }
        if (charge == totalChargeTime - totalAnimationTime)//The reason we do this is because we actually have a really really long first frame. This triggers it to start
        {
            shot1.currentFrame = 999999;
            shot1.currentAnimFrame = 999999;
        }
        if (charge < totalChargeTime - totalAnimationTime)
        {
            shot1.frames[shot1.currentAnim].spriteObject.active = false;
            shot1.currentFrame = 0;
            shot1.currentAnimFrame = 0;
            shot1.currentAnim = 0;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Animate();
        if (info.hit == 0)
        {
            charge += 1;
            if (charge == totalChargeTime - 1)
            {
                groundOption.GetComponent<Options>().charges = 1;
                airOption.GetComponent<Options>().charges = 1;
            }
        }
        if (notObama == false)
        {
            if (info.currentAnim != groundOption && info.currentAnim != airOption)
            {
                groundOption.GetComponent<SpeedLimit>().lastAnim = info.currentAnim;
            }
        }
    }
}
