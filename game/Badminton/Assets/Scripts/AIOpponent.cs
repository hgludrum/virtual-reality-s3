using UnityEngine;

public class AIOpponent : MonoBehaviour
{
    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    [Header("References")]
    [SerializeField] private Transform shuttlecock;
    [SerializeField] private Transform opponentRacket;

    [Header("Difficulty")]
    [SerializeField] private Difficulty difficulty = Difficulty.Medium;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float minZ = -2.59f;
    [SerializeField] private float maxZ = 2.59f;
    [SerializeField] private float aiMaxX = -0.1f;

    [Header("Racket")]
    [SerializeField] private float racketLift = 0.5f;
    [SerializeField] private float maxRacketLift = 2.5f;
    [SerializeField] private float racketRotationSpeed = 8f;
    [SerializeField] private float racketMinX = -3.2f;
    [SerializeField] private float racketMaxX = -0.2f;

    [Header("AI Hit")]
    [SerializeField] private float hitX = -2.5f;
    [SerializeField] private float landingX = 5.5f;
    [SerializeField] private float landingHeight = 0.3f;
    [SerializeField] private float netHeight = 1.65f;
    [SerializeField] private float netClearance = 1.2f;
    [SerializeField] private float hitHeight = 3.5f;
    [SerializeField] private float hitCooldown = 1f;

    private Rigidbody shuttleRb;
    private float lastHitTime;
    private Vector3 originalRacketLocalPosition;

    private float movementMultiplier;
    private float racketSpeedMultiplier;
    private float flightTime;
    private float heightMultiplier;

    private void Start()
    {
        if (shuttlecock != null)
            shuttleRb = shuttlecock.GetComponent<Rigidbody>();

        if (opponentRacket != null)
            originalRacketLocalPosition = opponentRacket.localPosition;

        ApplyDifficulty();
    }

    private void ApplyDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                movementMultiplier = 0.55f;
                racketSpeedMultiplier = 0.6f;
                flightTime = 2.6f;
                heightMultiplier = 0.75f;
                break;

            case Difficulty.Medium:
                movementMultiplier = 1f;
                racketSpeedMultiplier = 1f;
                flightTime = 2.2f;
                heightMultiplier = 1f;
                break;

            case Difficulty.Hard:
                movementMultiplier = 1.45f;
                racketSpeedMultiplier = 1.4f;
                flightTime = 1.8f;
                heightMultiplier = 1.2f;
                break;
        }
    }

    private void Update()
    {
        if (shuttlecock == null || shuttleRb == null)
            return;

        MoveAI();
        MoveRacketUnderShuttle();
        RotateRacket();
        CheckForHit();
    }

    private void MoveAI()
    {
        Vector3 target = transform.position;

        target.z = Mathf.Clamp(
            shuttlecock.position.z,
            minZ,
            maxZ
        );

        target.x = Mathf.Min(
            transform.position.x,
            aiMaxX
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * movementMultiplier * Time.deltaTime
        );

        Vector3 position = transform.position;
        position.x = Mathf.Min(position.x, aiMaxX);
        transform.position = position;
    }

    private void MoveRacketUnderShuttle()
    {
        if (opponentRacket == null)
            return;

        Vector3 targetWorldPosition = opponentRacket.position;

        targetWorldPosition.x = Mathf.Clamp(
            shuttlecock.position.x,
            racketMinX,
            racketMaxX
        );

        targetWorldPosition.z = Mathf.Clamp(
            shuttlecock.position.z,
            minZ,
            maxZ
        );

        float distanceToShuttle = Vector3.Distance(
            opponentRacket.position,
            shuttlecock.position
        );

        float yLift = Mathf.Lerp(
            racketLift,
            maxRacketLift,
            Mathf.Clamp01(1f - distanceToShuttle / 3f)
        );

        targetWorldPosition.y =
            transform.position.y +
            originalRacketLocalPosition.y +
            yLift;

        Vector3 targetLocalPosition =
            transform.InverseTransformPoint(targetWorldPosition);

        opponentRacket.localPosition = Vector3.MoveTowards(
            opponentRacket.localPosition,
            targetLocalPosition,
            moveSpeed *
            racketSpeedMultiplier *
            Time.deltaTime
        );
    }

    private void RotateRacket()
    {
        if (opponentRacket == null)
            return;

        Quaternion readyRotation =
            Quaternion.Euler(
                90f,
                0f,
                90f
            );

        opponentRacket.localRotation = Quaternion.Lerp(
            opponentRacket.localRotation,
            readyRotation,
            racketRotationSpeed *
            racketSpeedMultiplier *
            Time.deltaTime
        );
    }

    private void CheckForHit()
    {
        if (shuttleRb.linearVelocity.x >= 0f)
            return;

        if (shuttlecock.position.x <= hitX &&
            Time.time >= lastHitTime + hitCooldown)
        {
            HitShuttlecock();
        }
    }

    private void HitShuttlecock()
    {
        lastHitTime = Time.time;

        Vector3 start = shuttlecock.position;

        float startX = start.x;
        float startY =
            Mathf.Max(
                start.y,
                hitHeight * heightMultiplier
            );

        float gravity = Physics.gravity.y;

        float distance = landingX - startX;

        if (distance <= 0f)
            return;

        float vx = distance / flightTime;

        float vy =
            (
                landingHeight -
                startY -
                0.5f *
                gravity *
                flightTime *
                flightTime
            ) /
            flightTime;

        float targetZ = Mathf.Clamp(
            start.z,
            minZ + 0.25f,
            maxZ - 0.25f
        );

        float vz =
            (targetZ - start.z) /
            flightTime;

        float netTime =
            (0f - startX) /
            vx;

        float netY =
            startY +
            vy * netTime +
            0.5f *
            gravity *
            netTime *
            netTime;

        float requiredNetY =
            netHeight +
            netClearance;

        if (netY < requiredNetY)
        {
            float extraHeight =
                requiredNetY - netY;

            vy +=
                extraHeight *
                3f *
                heightMultiplier;
        }

        shuttleRb.linearVelocity =
            new Vector3(
                vx,
                vy,
                vz
            );
    }
}