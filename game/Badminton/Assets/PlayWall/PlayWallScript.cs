using UnityEngine;
using UnityEngine.InputSystem;


public class PlayWallScript : MonoBehaviour
{

    public GameObject ShuttlecockPrefab;
    public Transform spawnPoint;
    public InputActionProperty SpawnAction;
    public float shuttlecockInitailSpeed = 5f;
    public float spawnOffsetRange = 0.25f;


    private GameObject Shuttlecock;



    void Start()
    {
   

    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnAction.action != null && SpawnAction.action.WasPressedThisFrame())
        {
            SummonShuttlecock();
        }

    }


    public void SummonShuttlecock()
    {
        if (spawnPoint != null)
        {

            Vector3 spawnPointOffsetPosition = Vector3.zero;
            spawnPointOffsetPosition.x = spawnPoint.position.x + Random.Range(-spawnOffsetRange, spawnOffsetRange);
            spawnPointOffsetPosition.y = spawnPoint.position.y + Random.Range(-spawnOffsetRange, spawnOffsetRange);
            spawnPointOffsetPosition.z = spawnPoint.position.z;

            Shuttlecock = Instantiate(ShuttlecockPrefab, spawnPointOffsetPosition, spawnPoint.rotation);

            // 1. Get the Rigidbody component from the newly spawned GameObject
            Rigidbody rb = Shuttlecock.GetComponent<Rigidbody>();

            // 2. Check if the component exists to avoid NullReference exceptions
            if (rb != null)
            {
                rb.linearVelocity = spawnPoint.forward * -shuttlecockInitailSpeed;
            }
            Destroy(Shuttlecock, 10f);
        } 
        
    }
}
