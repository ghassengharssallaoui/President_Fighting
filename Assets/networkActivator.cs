using UnityEngine;
using Unity.Netcode;

public class networkActivator : NetworkBehaviour
{
    VersionDecider versionDecider;
    public GameObject offlineStuff;
    public GameObject onlineStuff;
    public GameObject blackScreen;
    public GameObject[] localOnlyStuffToDestroy;
    public GameObject[] serverOnlyStuffToDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        versionDecider = GameObject.Find("VersionDecider").GetComponent<VersionDecider>();
        if (versionDecider.server)
        {
            NetworkManager.Singleton.StartHost();
            foreach (GameObject g in serverOnlyStuffToDestroy)
            {
                Destroy(g);
            }
            Destroy(onlineStuff);

        }
        else if (versionDecider.client)
        {
            Destroy(offlineStuff);
            onlineStuff.active = true;
            NetworkManager.Singleton.StartClient();

        }
        else
        {
            Destroy(onlineStuff);
            foreach (GameObject g in localOnlyStuffToDestroy)
            {
                Destroy(g);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(blackScreen);
    }
}
