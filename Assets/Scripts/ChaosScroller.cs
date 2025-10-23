using UnityEngine;

public class SegmentedLevelScroller : MonoBehaviour
{
    [Header("Markers (children of this object)")]
    public Transform chaosStartMarker;
    public Transform chaosEndMarker;

    [Header("Normal Scroll (downwards)")]
    public float baseSpeed = 2.5f;
    public float accel = 8f;

    [Header("Chaos (vertical only)")]
    public Vector2 changeIntervalRange = new Vector2(0.8f, 2.0f);
    public float minSpeed = -2.0f;   // negative = UP
    public float maxSpeed = 6.0f;    // positive = DOWN
    [Range(0f,1f)] public float normalBias = 0.35f;
    [Range(0f,1f)] public float reverseChance = 0.20f;
    public float burstChance = 0.18f;
    public float burstMultiplier = 1.6f;
    public float burstDuration = 0.5f;

    [Header("Fairness near the end of chaos")]
    public float endBuffer = 3f;
    public float clampUpwardNearEnd = -0.5f;

    // NEW: guard rails
    [Header("Anti-stall Guard Rails")]
    public float maxBacktrack = 2.0f;        // don’t go more than this ABOVE chaos start
    public float maxChaosSeconds = 20f;      // force exit if chaos lasts too long
    public float forcedExitSpeed = 4f;       // positive speed used to break out

    Transform cam;
    float currentSpeed, targetSpeed, nextChangeAt;
    bool inBurst; float burstUntil;
    bool chaosStarted, chaosEnded;
    float chaosStartedAt = -1f;              // time we entered chaos

    void Start()
    {
        cam = Camera.main.transform;
        currentSpeed = baseSpeed;
        targetSpeed  = baseSpeed;
        ScheduleNextChange();
    }

    void Update()
    {
        float camY = cam.position.y;

        if (chaosStartMarker && !chaosStarted && chaosStartMarker.position.y <= camY) {
            chaosStarted = true;
            chaosStartedAt = Time.time;      // track chaos start time (NEW)
        }
        if (chaosEndMarker && !chaosEnded && chaosEndMarker.position.y <= camY)
            chaosEnded = true;

        bool inChaos = chaosStarted && !chaosEnded;

        if (inChaos)
        {
            // ---- Anti-stall checks (NEW) ----
            float progressFromStart = camY - chaosStartMarker.position.y; // <0 means we went back above start
            bool tooFarBack = progressFromStart < -maxBacktrack;
            bool chaosTooLong = (maxChaosSeconds > 0f) && (Time.time - chaosStartedAt > maxChaosSeconds);

            // If we’re too far above the start line or chaos ran too long, force downward exit
            if (tooFarBack || chaosTooLong)
            {
                inBurst = false;
                targetSpeed = Mathf.Max(forcedExitSpeed, baseSpeed);
            }
            else
            {
                if (Time.time >= nextChangeAt)
                    PickChaosTarget(camY);

                // Taper reverses as we approach the end line
                if (chaosEndMarker)
                {
                    float distToEnd = Mathf.Abs(chaosEndMarker.position.y - camY);
                    if (distToEnd <= endBuffer && targetSpeed < 0f)
                        targetSpeed = Mathf.Max(targetSpeed, clampUpwardNearEnd);
                }
            }

            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, accel * Time.deltaTime);
            if (inBurst && Time.time > burstUntil) inBurst = false;
        }
        else
        {
            // calm start & end
            inBurst = false;
            targetSpeed = baseSpeed;
            currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, accel * Time.deltaTime);
        }

        float speedScale = inBurst ? burstMultiplier : 1f;
        transform.Translate(Vector3.down * (currentSpeed * speedScale * Time.deltaTime), Space.World);
    }

    void PickChaosTarget(float camY)
    {
        if (Random.value < normalBias)
        {
            targetSpeed = baseSpeed;
        }
        else
        {
            bool chooseReverse = (Random.value < reverseChance);
            float spd;

            if (chooseReverse)
                spd = Random.Range(minSpeed, -0.4f);   // negative = UP
            else
                spd = Random.Range(Mathf.Max(0.8f, baseSpeed), maxSpeed);

            targetSpeed = spd;

            inBurst = false;
            if (Random.value < burstChance)
            {
                inBurst = true;
                burstUntil = Time.time + burstDuration;
            }
        }
        ScheduleNextChange();
    }

    void ScheduleNextChange()
    {
        nextChangeAt = Time.time + Random.Range(changeIntervalRange.x, changeIntervalRange.y);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (chaosStartMarker) Gizmos.DrawLine(chaosStartMarker.position + Vector3.left*50, chaosStartMarker.position + Vector3.right*50);
        Gizmos.color = Color.cyan;
        if (chaosEndMarker) Gizmos.DrawLine(chaosEndMarker.position + Vector3.left*50, chaosEndMarker.position + Vector3.right*50);
    }
#endif
}
