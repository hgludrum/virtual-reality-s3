using System.Buffers;
using UnityEngine;
using UnityEngine.InputSystem;
public class shuttlecockspawner : MonoBehaviour
{

    public GameObject ShuttlecockPrefab;
    public InputActionProperty SpawnAction;
    private GameObject Shuttlecock;



    void Update()
    {
        if (SpawnAction.action != null && SpawnAction.action.WasPressedThisFrame())
        {
            SummonShuttlecock();
        }

    }


    public void SummonShuttlecock()
    {
       

            Shuttlecock = Instantiate(ShuttlecockPrefab);

            
            Rigidbody rb = Shuttlecock.GetComponent<Rigidbody>();

            // 2. Check if the component exists to avoid NullReference exceptions
            if (rb != null)
            {
               
            }
           
        }

    }
