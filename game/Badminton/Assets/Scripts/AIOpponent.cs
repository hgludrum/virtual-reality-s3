using UnityEngine;

public class AIOpponent : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minZ = -2.8f;
    [SerializeField] private float maxZ = 2.8f;

    void Update()
    {
        float movement = Mathf.Sin(Time.time) * moveSpeed;

        Vector3 position = transform.position;
        position.z = Mathf.Clamp(position.z + movement * Time.deltaTime, minZ, maxZ);

        transform.position = position;
    }
}