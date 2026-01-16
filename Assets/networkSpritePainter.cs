using UnityEngine;
using Unity.Netcode;

public class networkSpritePainter : NetworkBehaviour
{
    spriteGod god;
    Sprite[] spriteList;
    Renderer[] rendererList;
    GameObject serverCam;
    GameObject clientCam;
    int waitCounter;
    public int numericID;
    public bool ui;
    public NetworkVariable<int> spriteID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> sortingLayer = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<bool> noChanges = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> posx = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> posy = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> posz = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> scalex = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> scaley = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> scalez = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> rotationx = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> rotationy = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> rotationz = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> colorr = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> colorg = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> colorb = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<float> colora = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        god = GameObject.Find("Sprite God").GetComponent<spriteGod>();
        if (IsServer)
        {
            serverCam = GameObject.Find("Main Camera");
        }
        if (!IsServer)
        {
            clientCam = GameObject.Find("Main Camera");
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
                if (ui == false)
                {
                    if (god.rendererList[numericID] != null)
                    {
                        noChanges.Value = false;
                        SpriteRenderer dummySprite = god.rendererList[numericID];

                        Transform parentt = dummySprite.transform.parent;
                        dummySprite.transform.SetParent(null);

                        Vector3 dummyPos = dummySprite.transform.position;
                        Vector3 dummyScale = dummySprite.transform.localScale;
                        dummySprite.transform.SetParent(parentt);

                        posx.Value = dummyPos.x;
                        posy.Value = dummyPos.y;
                        posz.Value = dummyPos.z;
                        scalex.Value = dummyScale.x;
                        scaley.Value = dummyScale.y;
                        scalez.Value = dummyScale.z;

                        spriteID.Value = 0; //default value

                        string[] sortArray = transform.parent.transform.parent.gameObject.GetComponent<sortingLayerArray>().array;
                        for (int i = 0; i < sortArray.Length; i++)
                        {
                            if (dummySprite.sortingLayerName == sortArray[i])
                            {
                                sortingLayer.Value = i;
                            }
                        }

                        if (dummySprite.transform.eulerAngles == new Vector3(0, 0, 0) && dummySprite.color == new Color(1, 1, 1, 1))
                        {
                            noChanges.Value = true;
                        }
                        else
                        {
                            rotationx.Value = dummySprite.transform.eulerAngles.x;
                            rotationy.Value = dummySprite.transform.eulerAngles.y;
                            rotationz.Value = dummySprite.transform.eulerAngles.z;
                            colorr.Value = dummySprite.color.r;
                            colorg.Value = dummySprite.color.g;
                            colorb.Value = dummySprite.color.b;
                            colora.Value = dummySprite.color.a;

                        }
                        for (int i = 0; i < god.spriteList.Length; i++)
                        {
                            if (god.spriteList[i] == dummySprite.sprite)
                            {
                                spriteID.Value = i;
                            }
                        }
                    }
                }
                else
                {
                    if (god.uiList[numericID] != null)
                    {
                        noChanges.Value = false;
                        SpriteRenderer dummySprite = god.uiList[numericID];

                        Transform parentt = dummySprite.transform.parent;
                        dummySprite.transform.SetParent(null);


                        Vector3 dummyPos = dummySprite.transform.position;
                        Vector3 dummyScale = dummySprite.transform.localScale;
                        dummySprite.transform.SetParent(parentt);



                        dummyPos = new Vector3(dummySprite.transform.position.x - serverCam.transform.position.x, dummySprite.transform.position.y - serverCam.transform.position.y, dummySprite.transform.position.z - serverCam.transform.position.z);

                        posx.Value = dummyPos.x;
                        posy.Value = dummyPos.y;
                        posz.Value = dummyPos.z;
                        scalex.Value = dummyScale.x;
                        scaley.Value = dummyScale.y;
                        scalez.Value = dummyScale.z;

                        spriteID.Value = 0; //default value

                        string[] sortArray = transform.parent.transform.parent.GetComponent<sortingLayerArray>().array;
                        print(dummySprite.sortingLayerName);
                        for (int i = 0; i < sortArray.Length; i++)
                        {
                            if (dummySprite.sortingLayerName == sortArray[i])
                            {
                                sortingLayer.Value = i;
                            }
                        }

                        if (dummySprite.transform.eulerAngles == new Vector3(0, 0, 0) && dummySprite.color == new Color(1, 1, 1, 1))
                        {
                            noChanges.Value = true;
                        }
                        else
                        {
                            rotationx.Value = dummySprite.transform.eulerAngles.x;
                            rotationy.Value = dummySprite.transform.eulerAngles.y;
                            rotationz.Value = dummySprite.transform.eulerAngles.z;
                            colorr.Value = dummySprite.color.r;
                            colorg.Value = dummySprite.color.g;
                            colorb.Value = dummySprite.color.b;
                            colora.Value = dummySprite.color.a;

                        }
                        for (int i = 0; i < god.spriteList.Length; i++)
                        {
                            if (god.spriteList[i] == dummySprite.sprite)
                            {
                                spriteID.Value = i;
                            }
                        }
                    }
                }
            }
            if (IsOwner)
            {
                if (ui == false)
                {
                    if (spriteID.Value != 0)
                    {
                        SpriteRenderer mySprite = god.painterList[numericID];
                        mySprite.transform.position = new Vector3(posx.Value, posy.Value, posz.Value);
                        mySprite.transform.localScale = new Vector3(scalex.Value, scaley.Value, scalez.Value);
                        mySprite.sprite = god.spriteList[spriteID.Value];
                        mySprite.sortingLayerName = transform.parent.transform.parent.GetComponent<sortingLayerArray>().array[sortingLayer.Value];
                        if (noChanges.Value == false)
                        {
                            mySprite.color = new Color(colorr.Value, colorg.Value, colorb.Value, colora.Value);
                            mySprite.transform.eulerAngles = new Vector3(rotationx.Value, rotationy.Value, rotationz.Value);
                        }
                    }
                }
                else
                {
                    SpriteRenderer mySprite = god.uipainterList[numericID]; //this is the only thing different between the two versions as of now.

                    if (spriteID.Value != 0 && mySprite.GetComponent<uiScaleWithAspectRatio>().enabled == false)
                    {

                        float screenheight = Screen.height;
                        float screenwidth = Screen.width;
                        float bastardRes = screenwidth / screenheight;//bastard because i'm pissed I even have to do this
                        float aspectMultiplier = 1.7777f / bastardRes;
                        aspectMultiplier = 1;
                        mySprite.transform.position = new Vector3(posx.Value / aspectMultiplier + clientCam.transform.position.x, posy.Value * aspectMultiplier + clientCam.transform.position.y, posz.Value + clientCam.transform.position.z);
                        mySprite.transform.localScale = new Vector3(scalex.Value, scaley.Value, scalez.Value);
                        if (mySprite.GetComponent<uiScaleWithAspectRatio>().enabled == false)
                        {
                            mySprite.GetComponent<uiScaleWithAspectRatio>().enabled = true;
                        }


                        mySprite.sprite = god.spriteList[spriteID.Value];
                        mySprite.sortingLayerName = transform.parent.transform.parent.GetComponent<sortingLayerArray>().array[sortingLayer.Value];

                        if (noChanges.Value == false)
                        {
                            mySprite.color = new Color(colorr.Value, colorg.Value, colorb.Value, colora.Value);
                            mySprite.transform.eulerAngles = new Vector3(rotationx.Value, rotationy.Value, rotationz.Value);
                        }

                    }
                    else
                    {
                        if(spriteID.Value == 0)
                        {
                            mySprite.sprite = god.spriteList[spriteID.Value];
                        }
                        mySprite.GetComponent<uiScaleWithAspectRatio>().enabled = false;

                    }
                }
            }
        }
    }
}