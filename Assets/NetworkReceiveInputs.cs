using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkReceiveInputs : NetworkBehaviour
{
    /// <summary>
    /// 

    public ReceiveInputs clientReceiver;
    public ReceiveInputs serverReceiver;
    public int serverTest;
    public GameObject dummyInput;
    public NetworkVariable<int>  lastLeft = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    //WHEN YOU RETURN: Need to update the "holding" feature and retool the buttons. I'm thinking maybe it's better to just have it be and "Attack" for now cus idk how remmapping works. Also find out how remapping works
    // Update is called once per frame
    public NetworkVariable<int>  holding0 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int>  holding1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int>  holding2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int>  holding3 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<bool> start = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> attack = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> special = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> jump = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> movement = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<bool> super = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);



    public NetworkVariable<int> holdingStart = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> holdingAttack = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> holdingSpecial = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> holdingJump = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> holdingMovement = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> holdingSuper = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    public NetworkVariable<bool> tapJump = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public NetworkVariable<float> moveVectorx = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> moveVectory = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> lastMoveVectorx = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> lastMoveVectory = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    /////


    public NetworkVariable<int> TRUEholdingStart = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholdingAttack = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholdingSpecial = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholdingJump = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholdingMovement = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholdingSuper = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);



    public NetworkVariable<int> TRUEholding0 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholding1 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholding2 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> TRUEholding3 = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    // Start is called before the first frame update
    void OnEnable()
    {

    }

    void Update()
    {
        if (clientReceiver == false && serverReceiver == false)
        {

            if (IsHost && IsOwner)//I am straight up killing this object if we're the host
            {
                Destroy(gameObject);
            }
            else
            {
                if (!IsServer)
                {
                    clientReceiver = GameObject.Find("onlineReceiver").GetComponent<ReceiveInputs>();
                }
                else
                {
                    serverReceiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
                    GameObject dummy = Instantiate(dummyInput);
                    serverReceiver.myInput = dummy.GetComponent<InputGod>();
                    dummy.GetComponent<InputGod>().enabled = false;
                }
            }
            if (false)//this is old stuff I might need if we switch back to using servers.
            {
                if (GameObject.Find("NetworkActivator").GetComponent<playerIdentifierForOnline>().players[0] == null)
                {
                    name = "P1NetworkPrefab";
                    if (IsServer)
                    {
                        GameObject.Find("NetworkActivator").GetComponent<playerIdentifierForOnline>().players[0] = GetComponent<NetworkReceiveInputs>();
                        serverReceiver = GameObject.Find("P1InputReceiver").GetComponent<ReceiveInputs>();
                        GameObject dummy = Instantiate(dummyInput);
                        serverReceiver.myInput = dummy.GetComponent<InputGod>();
                        dummy.GetComponent<InputGod>().enabled = false;
                    }
                }
                else
                {
                    name = "P2NetworkPrefab";
                    if (IsServer)
                    {
                        GameObject.Find("NetworkActivator").GetComponent<playerIdentifierForOnline>().players[1] = GetComponent<NetworkReceiveInputs>();
                        serverReceiver = GameObject.Find("P2InputReceiver").GetComponent<ReceiveInputs>();
                        GameObject dummy = Instantiate(dummyInput);
                        serverReceiver.myInput = dummy.GetComponent<InputGod>();
                        dummy.GetComponent<InputGod>().enabled = false;

                    }
                }
            }
            if (!IsServer)
            {
                serverTest = 1;

            }
        }
        if (IsOwner)
        {
            //TRUEholdingStart.Value = clientReceiver.TRUEholdingStart;
            //TRUEholdingAttack.Value = clientReceiver.TRUEholdingAttack;
            //TRUEholdingSpecial.Value = clientReceiver.TRUEholdingSpecial;
            //TRUEholdingJump.Value = clientReceiver.TRUEholdingJump;
            //TRUEholdingMovement.Value = clientReceiver.TRUEholdingMovement;
            //TRUEholdingSuper.Value = clientReceiver.TRUEholdingSuper;
            //TRUEholding0.Value = clientReceiver.TRUEholding[0];
            //TRUEholding1.Value = clientReceiver.TRUEholding[1];
            //TRUEholding2.Value = clientReceiver.TRUEholding[2];
            //TRUEholding3.Value = clientReceiver.TRUEholding[3];
        }
        if (IsOwner)
        {
            //holding0.Value = clientReceiver.holding[0];
            //holding1.Value = clientReceiver.holding[1];
            //holding2.Value = clientReceiver.holding[2];
            //holding3.Value = clientReceiver.holding[3];
            start.Value = clientReceiver.start;
            attack.Value = clientReceiver.attack;
            special.Value = clientReceiver.special;
            jump.Value = clientReceiver.jump;
            movement.Value = clientReceiver.movement;
            super.Value = clientReceiver.super;
            //holdingStart.Value = clientReceiver.holdingStart;
            //holdingAttack.Value = clientReceiver.holdingAttack;
            //holdingSpecial.Value = clientReceiver.holdingSpecial;
            //holdingJump.Value = clientReceiver.holdingJump;
            //holdingMovement.Value = clientReceiver.holdingMovement;
            //holdingSuper.Value = clientReceiver.holdingSuper;
            tapJump.Value = clientReceiver.tapJump;
            moveVectorx.Value = clientReceiver.moveVector.x;
            moveVectory.Value = clientReceiver.moveVector.y;
            lastMoveVectorx.Value = clientReceiver.lastMoveVector.x;
            lastMoveVectory.Value = clientReceiver.lastMoveVector.y;
        }
        if (IsServer)
        {
            //serverReceiver.holding[0] = holding0.Value;
            //serverReceiver.holding[1] = holding1.Value;
            //serverReceiver.holding[2] = holding2.Value;
            //serverReceiver.holding[3] = holding3.Value;
            serverReceiver.start = start.Value;
            serverReceiver.attack = attack.Value;
            serverReceiver.special = special.Value;
            serverReceiver.jump = jump.Value;
            serverReceiver.movement = movement.Value;
            serverReceiver.super = super.Value;
            //serverReceiver.holdingStart = holdingStart.Value;
            //serverReceiver.holdingAttack = holdingAttack.Value;
            //serverReceiver.holdingSpecial = holdingSpecial.Value;
            //serverReceiver.holdingJump = holdingJump.Value;
            // serverReceiver.holdingMovement = holdingMovement.Value;
            //serverReceiver.holdingSuper = holdingSuper.Value;
            serverReceiver.tapJump = tapJump.Value;
            serverReceiver.moveVector.x = moveVectorx.Value;
            serverReceiver.moveVector.y = moveVectory.Value;
            serverReceiver.lastMoveVector.x = lastMoveVectorx.Value;
            serverReceiver.lastMoveVector.y = lastMoveVectory.Value;
            //  serverReceiver.TRUEholdingStart = TRUEholdingStart.Value;
            //  serverReceiver.TRUEholdingAttack = TRUEholdingAttack.Value;
            // serverReceiver.TRUEholdingSpecial = TRUEholdingSpecial.Value;
            //   serverReceiver.TRUEholdingJump = TRUEholdingJump.Value;
            // serverReceiver.TRUEholdingMovement = TRUEholdingMovement.Value;
            //  serverReceiver.TRUEholdingSuper = TRUEholdingSuper.Value;
            //  serverReceiver.TRUEholding[0] = TRUEholding0.Value;
            //  serverReceiver.TRUEholding[1] = TRUEholding1.Value;
            //   serverReceiver.TRUEholding[2] = TRUEholding2.Value;
            //  serverReceiver.TRUEholding[3] = TRUEholding3.Value;
        }
    }
    void LateUpdate()
    {
        
    }
}
