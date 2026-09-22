using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NetworkGrababble : NetworkBehaviour
{
    private XRGrabInteractable interactable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(SetOwnershipOnGrab);
    }
    

    public void SetOwnershipOnGrab(SelectEnterEventArgs args)
    {
        if (NetworkManager.Singleton.IsConnectedClient && !IsOwner)
        {
            NetworkObject.ChangeOwnership(NetworkManager.Singleton.LocalClientId);
        }

    }
}
