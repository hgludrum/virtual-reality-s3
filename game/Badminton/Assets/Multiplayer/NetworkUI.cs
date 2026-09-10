using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{

    public Button clientButton;
    public Button hostButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
        hostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
