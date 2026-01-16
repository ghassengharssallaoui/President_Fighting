using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Collections;

public class networkTextTransfer : NetworkBehaviour
{
    spriteGod god;
    public int waitCounter;
    public int numericID;
    public NetworkVariable<float> parentPositionx = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> parentPositiony = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> parentPositionz = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public NetworkVariable<float> parentScalex = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> parentScaley = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> parentScalez = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public NetworkVariable<float> textRectx = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> textRecty = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> textColorr = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> textColorg = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> textColorb = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> textColora = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<FixedString64Bytes> textContent = new NetworkVariable<FixedString64Bytes>("", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> sortingLayer = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    GameObject clientCam;
    GameObject serverCam;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        god = GameObject.Find("Sprite God").GetComponent<spriteGod>();
        if (IsClient)
        {
            clientCam = GameObject.Find("Main Camera");
        }
        if (IsServer)
        {
            serverCam = GameObject.Find("Main Camera");
        }
    }

    // Update is called once per frame
    void Update()
    {
        waitCounter += 1;
        if (waitCounter > 5)
        {
            if (IsServer)
            {
                if (god.textList[numericID] != null)
                {
                    networkTextVariables dummy = god.textList[numericID];
                    
                    Vector3 dummyPos;
                    dummyPos = new Vector3(dummy.transform.position.x - serverCam.transform.position.x, dummy.transform.position.y - serverCam.transform.position.y, dummy.transform.position.z - serverCam.transform.position.z);

                    parentPositionx.Value = dummyPos.x;
                    parentPositiony.Value = dummyPos.y;
                    parentPositionz.Value = dummyPos.z;
                    parentScalex.Value = dummy.transform.localScale.x;
                    parentScaley.Value = dummy.transform.localScale.y;
                    parentScalez.Value = dummy.transform.localScale.z;


                    textColorr.Value = dummy.text.color.r;
                    textColorg.Value = dummy.text.color.g;
                    textColorb.Value = dummy.text.color.b;
                    textColora.Value = dummy.text.color.a;
                    textContent.Value = dummy.text.text;
                    //I took out the part about layers, since all text is on UI3 currently. 
                }
                else
                {
                    textColora.Value = 0;
                }
            }
            if (IsOwner)
            {
                networkTextVariables dummy = god.textPainterList[numericID];
                if (textColora.Value != 0)
                {
                    float screenheight = Screen.height;
                    float screenwidth = Screen.width;
                    float bastardRes = screenwidth / screenheight;//bastard because i'm pissed I even have to do this
                    float aspectMultiplier = 1.7777f / bastardRes;
                    aspectMultiplier = 1;
                    dummy.transform.position = new Vector3(parentPositionx.Value / aspectMultiplier + clientCam.transform.position.x, parentPositiony.Value * aspectMultiplier + clientCam.transform.position.y, parentPositionz.Value + clientCam.transform.position.z);
                    dummy.transform.localScale = new Vector3(parentScalex.Value, parentScaley.Value, parentScalez.Value);
                    dummy.text.color = new Color(textColorr.Value, textColorg.Value, textColorb.Value, textColora.Value);
                    dummy.text.text = textContent.Value.ToString();
                    if (dummy.GetComponent<uiScaleWithAspectRatio>().enabled == false)
                    {
                        dummy.GetComponent<uiScaleWithAspectRatio>().enabled = true;
                    }

                }
                else
                {
                    dummy.text.text = "";
                    dummy.GetComponent<uiScaleWithAspectRatio>().enabled = false;

                }
            }
        }
    }
}
