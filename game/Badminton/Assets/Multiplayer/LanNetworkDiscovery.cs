using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class LanNetworkDiscovery : MonoBehaviour
{
    private const string DiscoveryMessage = "BADMINTON_HOST";

    [SerializeField] private ushort gamePort = 7777;
    [SerializeField] private int discoveryPort = 47777;
    [SerializeField] private float broadcastInterval = 2f;

    [SerializeField] private string testHostAddress = "127.0.0.1";
    private readonly ConcurrentQueue<Action> mainThreadActions = new();
    private UdpClient listener;
    private Thread listenerThread;
    private Coroutine broadcastCoroutine;
    private volatile bool listening;

    private void Awake()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        NetworkManager.Singleton.OnTransportFailure += OnTransportFailure;
    }

    public void StartDirectClient()
    {
        if (NetworkManager.Singleton.IsListening)
            return;

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData(testHostAddress, gamePort);

        if (!NetworkManager.Singleton.StartClient())
        {
            Debug.LogError($"Could not connect directly to {testHostAddress}:{gamePort}.");
            return;
        }

        Debug.Log($"Connecting directly to {testHostAddress}:{gamePort}");
    }

    public void StartHost()
    {
        if (NetworkManager.Singleton.IsListening)
            return;

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData("0.0.0.0", gamePort, "0.0.0.0");

        if (!NetworkManager.Singleton.StartHost())
        {
            Debug.LogError("Could not start host.");
            return;
        }

        broadcastCoroutine = StartCoroutine(BroadcastHost());
        Debug.Log($"Host started on port {gamePort}");
    }

    public void StartClientSearch()
    {
        if (NetworkManager.Singleton.IsListening || listening)
            return;

        try
        {
            listener = new UdpClient(AddressFamily.InterNetwork);
            listener.Client.SetSocketOption(
                SocketOptionLevel.Socket,
                SocketOptionName.ReuseAddress,
                true);
            listener.Client.Bind(new IPEndPoint(IPAddress.Any, discoveryPort));
            listening = true;
            listenerThread = new Thread(ListenForHost) { IsBackground = true };
            listenerThread.Start();
            Debug.Log($"Searching for a LAN host on IPv4 UDP {discoveryPort}...");
        }
        catch (SocketException exception)
        {
            Debug.LogError($"Could not listen for LAN hosts on UDP {discoveryPort}: {exception.Message}");
            listener?.Close();
            listener = null;
        }
    }

    private void ListenForHost()
    {
        var endpoint = new IPEndPoint(IPAddress.Any, discoveryPort);

        try
        {
            while (listening)
            {
                var data = listener.Receive(ref endpoint);
                var message = Encoding.UTF8.GetString(data);

                if (message != DiscoveryMessage)
                    continue;

                var hostAddress = endpoint.Address.ToString();
                mainThreadActions.Enqueue(() =>
                {
                    Debug.Log($"LAN discovery packet received from {hostAddress}.");
                    ConnectToHost(hostAddress);
                });
                listening = false;
            }
        }
        catch (SocketException) when (!listening)
        {
        }
    }

    private void ConnectToHost(string hostAddress)
    {
        if (NetworkManager.Singleton.IsListening)
            return;

        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.SetConnectionData(hostAddress, gamePort);

        if (!NetworkManager.Singleton.StartClient())
            Debug.LogError($"Could not connect to host at {hostAddress}:{gamePort}.");
        else
            Debug.Log($"Connecting to host at {hostAddress}:{gamePort}");
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsClient)
            Debug.LogError($"Disconnected from host. ClientId: {clientId}");
    }

    private void OnTransportFailure()
    {
        Debug.LogError("Unity Transport failed. Check that all devices use the same Wi-Fi and UDP port 7777 is allowed.");
    }

    private IEnumerator BroadcastHost()
    {
        while (NetworkManager.Singleton.IsListening)
        {
            try
            {
                using var broadcaster = new UdpClient { EnableBroadcast = true };
                var message = Encoding.UTF8.GetBytes(DiscoveryMessage);
                var broadcastAddresses = GetBroadcastAddresses();

                foreach (var address in broadcastAddresses)
                    broadcaster.Send(message, message.Length,
                        new IPEndPoint(address, discoveryPort));

                Debug.Log($"LAN host announcement sent to: {string.Join(", ", broadcastAddresses)}");
            }
            catch (SocketException exception)
            {
                Debug.LogError($"Could not broadcast LAN host: {exception.Message}");
            }

            yield return new WaitForSeconds(broadcastInterval);
        }
    }

    private static System.Collections.Generic.List<IPAddress> GetBroadcastAddresses()
    {
        var addresses = new System.Collections.Generic.List<IPAddress>();

        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (networkInterface.OperationalStatus != OperationalStatus.Up)
                continue;

            foreach (var unicast in networkInterface.GetIPProperties().UnicastAddresses)
            {
                if (unicast.Address.AddressFamily != AddressFamily.InterNetwork ||
                    unicast.IPv4Mask == null)
                    continue;

                var ip = unicast.Address.GetAddressBytes();
                var mask = unicast.IPv4Mask.GetAddressBytes();
                var broadcast = new byte[4];

                for (var i = 0; i < broadcast.Length; i++)
                    broadcast[i] = (byte)(ip[i] | ~mask[i]);

                var broadcastAddress = new IPAddress(broadcast);
                if (!addresses.Contains(broadcastAddress))
                    addresses.Add(broadcastAddress);
            }
        }

        if (addresses.Count == 0)
            addresses.Add(IPAddress.Broadcast);

        return addresses;
    }

    private void Update()
    {
        while (mainThreadActions.TryDequeue(out var action))
            action();
    }

    private void OnDestroy()
    {
        if (broadcastCoroutine != null)
            StopCoroutine(broadcastCoroutine);

        listening = false;
        listener?.Close();
        listener = null;
        listenerThread = null;

        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
            NetworkManager.Singleton.OnTransportFailure -= OnTransportFailure;
        }
    }
}
