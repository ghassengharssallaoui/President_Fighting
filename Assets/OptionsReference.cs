using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptionsReference : MonoBehaviour
{
    public PlayerInfo infoscript;
    //public SubOption groundStickOptions; <--Turn these off if I ever need create a character that changes animations with movement alone
    //public SubOption airStickOptions;

    /// <summary>
    /// 
    /// </summary>

    /////
    public Option groundJumpOptions;
    public Option airJumpOptions;
    public Option groundAttackOptions;
    public Option airAttackOptions;
    public Option groundSpecialOptions;
    public Option airSpecialOptions;
    public Option groundMovementOptions;
    public Option airMovementOptions;
    public Option groundSuperOptions;
    public Option airSuperOptions;
    /// <summary>
    /// 
    /// </summary>
    ///
    public GameObject groundDefault;
    public GameObject airDefault;
    public GameObject landingDefault;
    public GameObject hitDefault;
    public GameObject flybackDefault;
    void FixOption(Option opt)
    {
        if(opt.Neutral.option == null)
        {
            opt.Neutral = opt.Forward;
        }
        if(opt.Down.option == null)
        {
            opt.Down = opt.Neutral;
        }
        if (opt.Up.option == null)
        {
            opt.Up = opt.Neutral;
        }
        if (opt.Forward.option == null)
        {
            if (opt.UpForward.option == null)
            {
                opt.UpForward = opt.Up;
            }
            if (opt.DownForward.option == null)
            {
                opt.DownForward = opt.Down;
            }
            opt.Forward = opt.Neutral;
        }
        else
        {
            if (opt.UpForward.option == null)
            {
                opt.UpForward = opt.Forward;
            }
            if (opt.DownForward.option == null)
            {
                opt.DownForward = opt.Forward;
            }
        }

    }
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoscript = dummy.GetComponent<PlayerInfo>();
        FixOption(groundJumpOptions);
        FixOption(airJumpOptions);
        FixOption(groundAttackOptions);
        FixOption(airAttackOptions);
        FixOption(groundSpecialOptions);
        FixOption(airSpecialOptions);
        FixOption(groundMovementOptions);
        FixOption(airMovementOptions);
        FixOption(groundSuperOptions);
        FixOption(airSuperOptions);
    }
}
