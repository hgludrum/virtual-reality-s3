using UnityEngine;

public class AIRacketHit : MonoBehaviour
{
    [SerializeField] private float hitForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.name.Contains("Shuttlecock"))
            return;

        Rigidbody shuttleRb = collision.rigidbody;

        if (shuttleRb == null)
            return;

        Vector3 returnDirection = Vector3.right + Vector3.up * 0.4f;

        shuttleRb.linearVelocity = returnDirection.normalized * hitForce;
    }
}