using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private TMP_InputField joinInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    async void Start()
    {
        DontDestroyOnLoad(gameObject);
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void CreateRelay()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(1);

        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        codeText.text = joinCode;
        var relayData = AllocationUtils.ToRelayServerData(allocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);
        NetworkManager.Singleton.StartHost();
    }
    public async void JoinRelay()
    {
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinInput.text);
        var relayData = AllocationUtils.ToRelayServerData(joinAllocation, "dtls");
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayData);
        NetworkManager.Singleton.StartClient();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
