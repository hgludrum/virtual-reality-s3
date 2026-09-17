using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RacketHit : MonoBehaviour
{
    [SerializeField] private float hitForceMultiplier = 1.5f;
    [SerializeField] private LayerMask shuttlecockLayer;

    private Vector3 lastPosition;
    private Vector3 racketVelocity;

    void FixedUpdate()
    {
        // Kinematic rigidbodies don't auto-track velocity, so compute it manually
        racketVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
        lastPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & shuttlecockLayer) == 0) return;

        Rigidbody shuttleRb = collision.rigidbody;
        if (shuttleRb == null) return;

        Vector3 contactNormal = collision.GetContact(0).normal;

        // Reflect incoming velocity off the racket face, then add racket's own swing velocity
        Vector3 outgoingVelocity = Vector3.Reflect(shuttleRb.linearVelocity, contactNormal);
        outgoingVelocity += racketVelocity * hitForceMultiplier;

        shuttleRb.linearVelocity = outgoingVelocity;
    }
}