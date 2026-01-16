using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileStick : MonoBehaviour
{
    public int range;
    RectTransform rect;
    float y;
    float x;
    public ReceiveInputs receiver;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        x = rect.anchoredPosition.x;
        y = rect.anchoredPosition.y;
        receiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dummyVector;
        dummyVector = new Vector2((rect.anchoredPosition.x - x) / range, (rect.anchoredPosition.y - y) / range);

        receiver.moveVector = dummyVector;
        receiver.tapJump = true;
    }
}
