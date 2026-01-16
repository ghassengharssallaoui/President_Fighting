using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class charInfo
{
    public string title;
    [TextAreaAttribute] public string description;
    public Sprite headerSprite;
    public float headerSize = 1;
    public Vector3 headerDisplacement;
    public Sprite input1;
    public Sprite input2;
    public Sprite input3;
    public Sprite input4;

    //any other info
}