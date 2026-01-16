using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horizontalMenu : MonoBehaviour
{
    public VerticalMenu[] verts;
    public int player;
    ReceiveInputs receiver;
    public int currentVert;
    public int lastValue;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (player == 2)
        {
            receiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
        }
        else
        {
            receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
        }
        foreach (VerticalMenu v in verts)
        {
            v.player = player;
            v.dontResetSelection = true;
            v.gameObject.active = false;
        }
        verts[currentVert].gameObject.active = true;
        verts[currentVert].currentSelection = lastValue;
    }
    void MoveLeft()
    {
        int currentSelection;
        currentSelection = verts[currentVert].currentSelection;
        verts[currentVert].gameObject.active = false;
        currentVert += -1;
        verts[currentVert].currentSelection = currentSelection;
        verts[currentVert].gameObject.active = true;
    }
    void MoveRight()
    {
        int currentSelection;
        currentSelection = verts[currentVert].currentSelection;
        verts[currentVert].gameObject.active = false;
        currentVert += 1;
        verts[currentVert].currentSelection = currentSelection;
        verts[currentVert].gameObject.active = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (receiver.holding[2] == 3 && currentVert > 0)
        {
            MoveLeft();
        }
        if (receiver.holding[3] == 3 && currentVert < verts.Length -1)
        {
            MoveRight();
        }
        lastValue = verts[currentVert].currentSelection;

    }
    void OnDisable()
    {
    }
}
