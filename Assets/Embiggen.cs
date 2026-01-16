using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embiggen : MonoBehaviour
{
    public GameObject[] inputs;
    public float trueScale;
    // Start is called before the first frame update
    void OnEnable()
    {
        trueScale = inputs[0].transform.localScale.x;
        foreach (GameObject i in inputs)
        {
            i.transform.localScale = new Vector3(i.transform.localScale.x * 2, i.transform.localScale.x * 2, i.transform.localScale.x * 2);
        }
        for(int i = 0; i < inputs.Length; i++)
        {
            float positionChange = -3f;
            positionChange += i;
            inputs[i].transform.position += new Vector3(positionChange * inputs[i].transform.localScale.x / 4, 0, 0);
        }
    }
    void OnDisable()
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            float positionChange = -3f;
            positionChange += i;
            inputs[i].transform.position += new Vector3(positionChange * -inputs[i].transform.localScale.x /4, 0, 0);
        }
        foreach (GameObject i in inputs)
        {
            i.transform.localScale = new Vector3(trueScale, trueScale, trueScale);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
