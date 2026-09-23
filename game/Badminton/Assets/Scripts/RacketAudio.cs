using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RacketAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] hitBallClips;
    [SerializeField] private AudioClip swingWhooshClip;

    [Header("Swing Detection")]
    [SerializeField] private float swingVelocityThreshold = 2.5f;
    [SerializeField] private float swingCooldown = 0.3f;

    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float lastSwingTime;

    void Start()
    {
        // Rättat till här:
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    void Update()
    {
        DetectSwing();
    }

    private void DetectSwing()
    {
        float currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        lastPosition = transform.position;

        if (currentSpeed > swingVelocityThreshold && Time.time >= lastSwingTime + swingCooldown)
        {
            if (swingWhooshClip != null)
            {
                audioSource.PlayOneShot(swingWhooshClip, 0.7f);
            }
            lastSwingTime = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.name.Contains("Shuttlecock"))
        {
            if (hitBallClips != null && hitBallClips.Length > 0)
            {
                int randomIndex = Random.Range(0, hitBallClips.Length);
                audioSource.PlayOneShot(hitBallClips[randomIndex], 1.0f);
            }
        }
    }
}
