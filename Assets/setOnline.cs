using UnityEngine;

public class setOnline : MonoBehaviour
{
    public bool server;
    public bool client;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameObject.Find("VersionDecider").GetComponent<VersionDecider>().server = server;
        GameObject.Find("VersionDecider").GetComponent<VersionDecider>().client = client;
        if (client)
        {
            DontDestroyOnLoad(GameObject.Find("P1Input"));
            DontDestroyOnLoad(GameObject.Find("P1InputReceiver"));
            DontDestroyOnLoad(GameObject.Find("DummyReceiver"));
            GameObject.Find("P1InputReceiver").name = "onlineReceiver";
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        
    }
}
