using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class targetScript : MonoBehaviour
{
    private int score = 0;

    private float lastScoreTime = 0f;
    public float scoreCooldown = 1f; // 1 second cooldown

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shuttlecock"))
        {
            // Checks if enough time has passed since the last score
            if (Time.time - lastScoreTime > scoreCooldown)
            {
                score++;
                lastScoreTime = Time.time; // Resets the timer

                Debug.Log("The shuttlecock passed through the zone! Current score: " + score);
            }
        }
    }
}
