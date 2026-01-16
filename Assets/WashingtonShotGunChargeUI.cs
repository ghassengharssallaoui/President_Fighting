using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingtonShotGunChargeUI : MonoBehaviour
{
    public int totalChargeTime;
    public int chargeAnimationTime;
    public JustAnimate shot1;
    public JustAnimate shot2;
    public int[] charges = new int[2];
    public PlayerInfo info;
    public int infoCharges;
    public int lastCharges;
    void Start()
    {
        charges[0] = totalChargeTime;
        charges[1] = totalChargeTime;
        lastCharges = 2;
        if(info.player == 1)
        {
            GetComponent<followExactly>().target = GameObject.Find("WashingtonUIPlayer1");
        }
        if (info.player == 2)
        {
            GetComponent<followExactly>().target = GameObject.Find("WashingtonUIPlayer2");
        }
    }
    void Animate()
    {
        if (charges[1] == totalChargeTime - chargeAnimationTime)
        {
            shot2.currentFrame = 999999;
            shot2.currentAnimFrame = 999999;
        }
        if (charges[0] == totalChargeTime - chargeAnimationTime)
        {
            shot1.currentFrame = 999999;
            shot1.currentAnimFrame = 999999;
        }
        if (charges[1] == 0)
        {
            shot2.frames[shot1.currentAnim].spriteObject.active = false;
            shot2.currentFrame = 0;
            shot2.currentAnimFrame = 0;
            shot2.currentAnim = 0;
        }
    }
    void FixedUpdate()
    {
        GameObject g = info.reference.groundMovementOptions.Neutral.option;
        GameObject airg = info.reference.airMovementOptions.Neutral.option;
        infoCharges = g.GetComponent<Options>().charges;
        if (lastCharges == 2)
        {
            if(infoCharges < 2)
            {
                charges[1] = 0;
                lastCharges = 1;
            }
        }
        if(lastCharges == 1)
        {
            if(infoCharges < 1)
            {
                shot1.frames[shot1.currentAnim].spriteObject.active = false;
                shot1.currentFrame = shot2.currentFrame;
                shot1.currentAnimFrame = shot2.currentAnimFrame;
                shot1.currentAnim = shot2.currentAnim;
                charges[0] = charges[1];
                charges[1] = 0;
                lastCharges = 0;
            }
        }
        Animate();
        if (info.hit == 0)
        {
            if (charges[0] < totalChargeTime)
            {
                if (charges[0] == totalChargeTime - 1)
                {
                    g.GetComponent<Options>().charges = 1;
                    airg.GetComponent<Options>().charges = 1;
                    lastCharges = 1;
                }
                charges[0] += 1;
            }
            else if (charges[1] < totalChargeTime)
            {
                if (charges[1] == totalChargeTime - 1)
                {
                    g.GetComponent<Options>().charges = 2;
                    airg.GetComponent<Options>().charges = 2;
                    lastCharges = 2;
                }
                charges[1] += 1;
            }
        }
         
    }
}
