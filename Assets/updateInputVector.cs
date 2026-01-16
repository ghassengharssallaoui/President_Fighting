using UnityEngine;

public class updateInputVector : MonoBehaviour
{

    public rememberInputVector script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        script.vector = dummy.GetComponent<PlayerInfo>().receiver.moveVector;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
