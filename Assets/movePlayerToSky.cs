using UnityEngine;

public class movePlayerToSky : MonoBehaviour
{
    PlayerInfo infoScript;
    public Vector3 finalPos;
    public bool onDisable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        infoScript = dummy.GetComponent<PlayerInfo>();
    }
    void OnEnable()
    {
        if(onDisable == false)
        {
            infoScript.transform.position = finalPos;
        }
    }
    void OnDisable()
    {
        if (onDisable == true)
        {
            infoScript.transform.position = finalPos;
        }
    }

}
