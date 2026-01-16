using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class cpuCommand 
{
    public int start;
    public int end = 1;
    public bool stop;
    public Vector2 moveVector;
    public bool Attack;
    public bool Special;
    public bool Jump;
    public bool Super;
    public bool Movement;
    public bool trigger2;
    public bool trigger3;
    public bool trigger4;
}
