using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Option
{
    public bool lockOut;
    public int additionalActionableFrame;
    public SubOption Neutral;
    public SubOption Forward;
    public SubOption Up;
    public SubOption Down;
    public SubOption UpForward;
    public SubOption DownForward;
}
