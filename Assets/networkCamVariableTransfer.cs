using UnityEngine;
using Unity.Netcode;

public class networkCamVariableTransfer : NetworkBehaviour
{
    public BetterCameraMovement serverCam;
    public networkCamMovement clientCam;
    public NetworkVariable<float> posX = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> posY = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> posZ = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (IsServer)
        {
            serverCam = GameObject.Find("NetworkActivator").GetComponent<camIdentifierForOnline>().cam;
        }
        if (!IsServer)
        {
            clientCam = GameObject.Find("Main Camera").GetComponent<networkCamMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsServer)
        {
            posX.Value = serverCam.transform.position.x;
            posY.Value = serverCam.transform.position.y;
            posZ.Value = serverCam.transform.position.z;
        }
        if (!IsServer)
        {
            if (clientCam.enabled == false)
            {
                clientCam.transform.position = new Vector3(posX.Value, posY.Value, posZ.Value);
            }
            clientCam.starterVector.x = posX.Value;
            clientCam.starterVector.y = posY.Value;
            clientCam.starterVector.z = posZ.Value;
        }
    }
}
