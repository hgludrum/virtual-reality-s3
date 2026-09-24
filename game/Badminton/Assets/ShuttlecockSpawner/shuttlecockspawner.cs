
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class shuttlecockspawner : MonoBehaviour
{

    public GameObject ShuttlecockPrefab;
    private InputAction spawnAction;

    private void Start()
    {
        RegisterNetworkPrefab();
    }

    private void OnEnable()
    {
        // On Quest, A is the right Touch controller's primary button. Keep this
        // action local so a stale Inspector InputActionReference cannot override
        // the controller binding.
        spawnAction = new InputAction("Spawn Shuttlecock", InputActionType.Button);
        spawnAction.AddBinding("<XRController>{LeftHand}/primaryButton");
        spawnAction.AddBinding("<OculusTouchController>{LeftHand}/primaryButton");
        spawnAction.Enable();
    }

    private void OnDisable()
    {
        spawnAction?.Disable();
        spawnAction?.Dispose();
        spawnAction = null;
    }

    private void Update()
    {
        if (spawnAction != null && spawnAction.WasPressedThisFrame())
            SummonShuttlecock();
    }

    public void SummonShuttlecock()
    {
        if (ShuttlecockPrefab == null)
        {
            Debug.LogError("ShuttlecockPrefab is not assigned.", this);
            return;
        }

        var networkManager = NetworkManager.Singleton;
        var shuttlecock = Instantiate(ShuttlecockPrefab, transform.position, transform.rotation);
        var networkObject = shuttlecock.GetComponent<NetworkObject>();
        if (networkObject != null && networkManager != null && networkManager.IsListening)
            networkObject.Spawn();
    }

    private void RegisterNetworkPrefab()
    {
        if (ShuttlecockPrefab == null)
            return;

        var networkManager = NetworkManager.Singleton;
        if (networkManager == null)
            return;

        if (ShuttlecockPrefab.GetComponent<NetworkObject>() != null)
            networkManager.AddNetworkPrefab(ShuttlecockPrefab);
    }
}
