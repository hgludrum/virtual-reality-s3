using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using System.Threading.Tasks;
using UnityEngine.Events;

public class ConnectionManager : MonoBehaviour
{
    public int maxPlayer = 4;
    private ISession currentSession;

    public enum ConnectionState { Conneting, Failed, Success, Disconnecting, Disconnected }
    public UnityEvent<ConnectionState> OnConnectionEvent;
    public async void QuickJoinOrCreateSession()
    {
        OnConnectionEvent.Invoke(ConnectionState.Conneting);
        try
        {
            bool authenticaded = await Authenticate();

            if (!authenticaded)
            {
                Debug.LogError("Authentication failed. Cannot join or create session.");

                OnConnectionEvent.Invoke(ConnectionState.Failed);

                return;
            }

            var availableSessions = await MultiplayerService.Instance.QuerySessionsAsync(new QuerySessionsOptions());

            foreach (var session in availableSessions.Sessions)
            {
                if (session.AvailableSlots <= 0) 
                    continue;

                try 
                {
                    currentSession = await MultiplayerService.Instance.JoinSessionByIdAsync(session.Id);
                    OnConnectionEvent.Invoke(ConnectionState.Success);
                    return;
                }

                catch
                {
                    continue;
                }
            }

            var options = new SessionOptions { MaxPlayers = maxPlayer }.WithDistributedAuthorityNetwork();
          
            currentSession = await MultiplayerService.Instance.CreateSessionAsync(options);
            OnConnectionEvent.Invoke(ConnectionState.Success);


        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            OnConnectionEvent.Invoke(ConnectionState.Failed);
        }
    }


    public async void Disconnect()
    {
      if (currentSession != null)
        {
            OnConnectionEvent.Invoke(ConnectionState.Disconnecting);

            await currentSession.LeaveAsync();
            currentSession = null;

            OnConnectionEvent.Invoke(ConnectionState.Disconnected);
        }
        
    }


    public async Task<bool> Authenticate()
    {
        try 
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                var options = new InitializationOptions();
                await UnityServices.InitializeAsync(options);
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

                return true;      
            
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to initialize Unity Services: {e}");
            return false;
        }
    }
}
