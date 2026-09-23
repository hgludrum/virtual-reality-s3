
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class shuttlecockspawner : MonoBehaviour
{

    public GameObject ShuttlecockPrefab;
    public InputActionProperty SpawnAction;
    private bool spawnActionEnabledBySpawner;

    private void Start()
    {
        RegisterNetworkPrefab();
    }

    private void OnEnable()
    {
        var action = SpawnAction.action;
        if (action == null)
            return;

        // The prefab stores a local action, so provide the Quest right-hand A button
        // binding when no action has been configured in the Inspector.
        if (action.bindings.Count == 0)
        {
            action.AddBinding("<XRController>{RightHand}/{PrimaryButton}");
            action.AddBinding("<OculusTouchController>{RightHand}/primaryButton");
        }

        if (!action.enabled)
        {
            action.Enable();
            spawnActionEnabledBySpawner = true;
        }
    }

    private void OnDisable()
    {
        if (spawnActionEnabledBySpawner && SpawnAction.action != null)
            SpawnAction.action.Disable();

        spawnActionEnabledBySpawner = false;
    }

    private void Update()
    {
        if (SpawnAction.action != null && SpawnAction.action.WasPressedThisFrame())
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
        var networkObjectPrefab = ShuttlecockPrefab.GetComponent<NetworkObject>();

        if (networkObjectPrefab != null && (networkManager == null || !networkManager.IsServer))
            return;

        var shuttlecock = Instantiate(ShuttlecockPrefab, transform.position, transform.rotation);
        var networkObject = shuttlecock.GetComponent<NetworkObject>();
        if (networkObject != null)
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
