using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    public Button quickJoinButton;
    public Button disconnectButton;
    public ConnectionManager connectionManager;

    public GameObject offlinePanel;
    public GameObject connectingPanel;
    public GameObject connectedPanel;



    [SerializeField] private LanNetworkDiscovery networkDiscovery;

    private void Start()
    {
        if (quickJoinButton == null || networkDiscovery == null)
        {
            Debug.LogError("NetworkUI is missing a button or LanNetworkDiscovery reference.", this);
            enabled = false;
            return;
        }




        quickJoinButton.onClick.AddListener(connectionManager.QuickJoinOrCreateSession);
        disconnectButton.onClick.AddListener(connectionManager.Disconnect);

        UpdateUI(ConnectionManager.ConnectionState.Disconnected);

        connectionManager.OnConnectionEvent.AddListener(UpdateUI);
    }


    public void UpdateUI(ConnectionManager.ConnectionState state)
    {
       offlinePanel.SetActive(state == ConnectionManager.ConnectionState.Disconnected || state == ConnectionManager.ConnectionState.Failed);
       connectingPanel.SetActive(state == ConnectionManager.ConnectionState.Conneting || state == ConnectionManager.ConnectionState.Disconnecting);
       connectedPanel.SetActive(state == ConnectionManager.ConnectionState.Success);
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


}