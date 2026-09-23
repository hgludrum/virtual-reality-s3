using UnityEngine;

public class AIOpponent : MonoBehaviour
{
    [SerializeField] private Transform shuttlecock;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float minZ = -2.8f;
    [SerializeField] private float maxZ = 2.8f;

    [Header("AI Hit")]
    [SerializeField] private float hitX = -2.5f;
    [SerializeField] private float hitForce = 5f;
    [SerializeField] private float upwardForce = 2f;
    [SerializeField] private float hitCooldown = 1f;

    private Rigidbody shuttleRb;
    private float lastHitTime;

    private void Start()
    {
        if (shuttlecock != null)
            shuttleRb = shuttlecock.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (shuttlecock == null || shuttleRb == null)
            return;

        // Move the AI racket toward the shuttlecock along the Z axis.
        Vector3 target = transform.position;
        target.z = Mathf.Clamp(shuttlecock.position.z, minZ, maxZ);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        // The shuttle is coming toward the AI.
        if (shuttleRb.linearVelocity.x < 0 &&
            shuttlecock.position.x <= hitX &&
            Time.time >= lastHitTime + hitCooldown)
        {
            HitShuttlecock();
        }
    }

    private void HitShuttlecock()
    {
        lastHitTime = Time.time;

        Vector3 direction = new Vector3(1f, upwardForce, 0f).normalized;

        shuttleRb.linearVelocity = direction * hitForce;
    }
}