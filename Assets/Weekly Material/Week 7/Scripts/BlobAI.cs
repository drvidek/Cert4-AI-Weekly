using UnityEngine;

public class BlobAI : MonoBehaviour
{
    // All possible states our blob could be in
    public enum State
    {
        Idle,
        Walk,
        Hunt,
        Flee
    }

    // The current state this blob is in
    public State stateCurrent;
    // How fast to move during the Walk state
    public float walkSpeed = 1.5f;
    // How fast to move during the Hunt state
    public float huntSpeed = 3f;
    // How fast to move during the Flee state
    public float fleeSpeed = 2.5f;
    // The maximum distance to wander from your current location
    public float wanderRadius = 10f;
    // The closest the player can get before they are 'seen' by the blob
    public float awarenessRange = 5f;
    // The furthest the player can be before the blob stops responding to them
    public float disengageRange = 7f;
    // The shortest the blob can wait during Idle state
    public float durationIdleMin = .5f;
    // The longest the blob can wait during Idle state
    public float durationIdleMax = 1.5f;
    // How long the blob currently has left to wait in an Idle state
    private float durationIdleCurrent;
    // Reference to the Player's blob size
    private BlobSize playerBlob;
    // Reference to this blob's size
    private BlobSize mySize;
    // The current target for wandering
    private Vector3 currentTarget;

    void Start()
    {
        mySize = GetComponent<BlobSize>();
        SetRandomTarget();
    }

    void Update()
    {
        CheckForPlayerNearby();

        // Choose which function to run based on our current state
        switch (stateCurrent)
        {
            // If our current state is Idle...
            case State.Idle:
                // Do our 'Idle' function
                Idle();
                break;

            case State.Walk:
                Walk();
                break;

            case State.Hunt:
                Hunt();
                break;

            case State.Flee:
                Flee();
                break;
        }
    }

    void ChangeState(State newState)
    {
        stateCurrent = newState;
    }

    void Idle()
    {
        // Count down our idle time towards 0
        durationIdleCurrent -= Time.deltaTime;

        // Once we reach 0...
        if (durationIdleCurrent <= 0)
        {
            // Choose a new random wander target
            SetRandomTarget();
            // Change to the Walk state
            ChangeState(State.Walk);
        }
    }

    void Walk()
    {
        // Move towards the current target
        MoveTowards(currentTarget, walkSpeed);

        // If we arrive at the current target...
        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            // Set a random Idle wait time
            SetRandomDuration();
            // Change to the Idle state
            ChangeState(State.Idle);
        }
    }

    void Hunt()
    {
        // Move towards the player 
        MoveTowards(playerBlob.transform.position, huntSpeed);

        // If the player leaves active range...
        if (!IsPlayerInRange(disengageRange))
        {
            // Go back to Idle state
            SetRandomDuration();
            ChangeState(State.Idle);
        }
    }

    void Flee()
    {
        // A bit cheeky, but using a negative speed here will make us move away instead of towards the player
        MoveTowards(playerBlob.transform.position, -fleeSpeed);

        // If the player leaves the range
        if (!IsPlayerInRange(disengageRange))
        {
            // Go back to Idle
            SetRandomDuration();
            ChangeState(State.Idle);
        }
    }

    /// <summary>
    /// Move this transform towards the given point, by the speed provided in units per second.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    void MoveTowards(Vector3 target, float speed)
    {
        // Calculate and normalise the direction
        Vector3 direction = target - transform.position;
        direction.Normalize();

        // Update our position based on our speed and direction
        transform.position += speed * Time.deltaTime * direction;
    }

    /// <summary>
    /// Set a random duration for the Idle state
    /// </summary>
    void SetRandomDuration()
    {
        // Set the current Idle time to a random number between our min and max
        durationIdleCurrent = Random.Range(durationIdleMin, durationIdleMax);
    }

    /// <summary>
    /// Set a random target for the Walk state
    /// </summary>
    void SetRandomTarget()
    {
        // Get a random point inside a circle, where the circle centre is 0, 0
        Vector3 randomPoint = Random.insideUnitCircle;

        // Using the random point and our maximum wander radius, 
        // set a new target relative to our current position 
        currentTarget = transform.position + (wanderRadius * randomPoint);
    }

    /// <summary>
    /// Returns true if the distance from this blob to the player is within the given distance, else returns false
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    bool IsPlayerInRange(float distance)
    {
        return Vector3.Distance(transform.position, playerBlob.transform.position) <= distance;
    }

    // Checks if the player is nearby, and assesses whether to flee or hunt if so.
    void CheckForPlayerNearby()
    {
        // If the player is within the range of awareness...
        if (IsPlayerInRange(awarenessRange))
        {
            // If the player is a bigger blob...
            if (playerBlob.radius > mySize.radius)
            {
                // Run away
                ChangeState(State.Flee);
            }
            else // Else, attack the player
            {
                ChangeState(State.Hunt);
            }
        }
    }
}
