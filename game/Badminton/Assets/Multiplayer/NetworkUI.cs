using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button clientButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private LanNetworkDiscovery networkDiscovery;

    private void Start()
    {
        if (clientButton == null || hostButton == null || networkDiscovery == null)
        {
            Debug.LogError("NetworkUI is missing a button or LanNetworkDiscovery reference.", this);
            enabled = false;
            return;
        }

        clientButton.onClick.AddListener(OnClientButtonClicked);
        hostButton.onClick.AddListener(OnHostButtonClicked);
    }

    private void OnClientButtonClicked()
    {
        //networkDiscovery.StartClientSearch();
        networkDiscovery.StartDirectClient();
    }

    private void OnHostButtonClicked()
    {
        networkDiscovery.StartHost();
    }

    private void OnDestroy()
    {
        if (clientButton != null)
            clientButton.onClick.RemoveListener(OnClientButtonClicked);

        if (hostButton != null)
            hostButton.onClick.RemoveListener(OnHostButtonClicked);
    }
}