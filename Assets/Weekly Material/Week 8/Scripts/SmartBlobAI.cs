using UnityEngine;

public class SmartBlobAI : MonoBehaviour
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
    // Stores a target for the blob to more towards
    private Vector3 currentTarget;
    // Reference this blob's BlobSize component
    private BlobSize mySize;
    // Reference this blob's BlobAwareness component
    private BlobAwareness blobAwareness;

    void Start()
    {
        mySize = GetComponent<BlobSize>();

        // The blob awareness component is in a child object
        blobAwareness = GetComponentInChildren<BlobAwareness>();
        SetRandomTarget();
    }

    void Update()
    {
        AssessAwareness();
        switch (stateCurrent)
        {
            case State.Idle:
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
        durationIdleCurrent -= Time.deltaTime;
        if (durationIdleCurrent <= 0)
        {
            SetRandomTarget();
            ChangeState(State.Walk);
        }
    }

    void Walk()
    {
        MoveTowards(currentTarget, walkSpeed);

        if (Vector3.Distance(transform.position, currentTarget) < 0.1f)
        {
            SetRandomDuration();
            ChangeState(State.Idle);
        }
    }

    void Hunt()
    {
        MoveTowards(currentTarget, huntSpeed);
    }

    void Flee()
    {
        MoveTowards(currentTarget, fleeSpeed);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = target - transform.position;
        direction.Normalize();
        transform.position += speed * Time.deltaTime * direction;
    }

    void SetRandomDuration()
    {
        durationIdleCurrent = Random.Range(durationIdleMin, durationIdleMax);
    }

    void SetRandomTarget()
    {
        Vector2 randomPoint = Random.insideUnitCircle;
        currentTarget = wanderRadius * randomPoint;
    }

    // Determine if the blob should flee, hunt, or wander, depending on what other blobs are around.
    void AssessAwareness()
    {
        // If no one is nearby...
        if (!blobAwareness.IsAnyoneNearby())
        {
            // Just go back to Idle/Walk
            ReturnToIdle();

            // Don't do anything else in this function
            return;
        }

        // To figure out the overall direction we want to go
        Vector3 directionAwayFromDanger = new();

        // Track the overall threat of our surroundings
        int blobsBiggerThanMe = 0;

        // Track the smallest blob we've found
        BlobSize smallestBlob = null;

        // Look at all the blobs in our awareness range
        foreach (BlobSize blob in blobAwareness.nearbyBlobs)
        {
            // If the blob is bigger than me
            if (blob.radius > mySize.radius)
            {
                // Count up how many blobs are bigger than me
                blobsBiggerThanMe++;

                // Figure out the direction away from that blob
                Vector3 directionAway = transform.position - blob.transform.position;

                //Add it to the overall direction I should move to avoid bigger blobs
                directionAwayFromDanger += directionAway.normalized;
            }

            // If the blob is smaller than me...
            else if (blob.radius < mySize.radius)
            {
                // If we don't currently have a smallest blob...
                if (smallestBlob == null)
                {
                    // now we do!
                    smallestBlob = blob;
                }
                // Otherwise, if this blob is smaller than the current smallest blob...
                else if (blob.radius < smallestBlob.radius)
                {
                    // This blob is the new smallest blob
                    smallestBlob = blob;
                }
            }
        }

        // If there are more than 50% bigger blobs...
        if (blobsBiggerThanMe >= blobAwareness.nearbyBlobs.Count / 2f)
        {
            // Normalise our flee direction
            directionAwayFromDanger.Normalize();

            // Figure out a "target" by adding the direction to our current position
            // We're multiplying by fleeSpeed for Gizmo drawing purposes but just adding the direction would work totally fine
            currentTarget = transform.position + (directionAwayFromDanger * fleeSpeed);

            // Run away
            ChangeState(State.Flee);

        }
        // Else if we found a smaller blob
        else if (smallestBlob)
        {
            // Target that blob
            currentTarget = smallestBlob.transform.position;

            // Change to hunting
            ChangeState(State.Hunt);
        }
        else
        {
            // Else, we should go back to Idle/Walk
            ReturnToIdle();
        }
    }

    // Safely return to the idle/walk loop without resetting it if it's currently active
    // This function is only necessary because of how we're managing our State Machine (running functions every Update)
    // A more sophistocated machine could avoid this problem but this is a totally fine workaround for us here
    void ReturnToIdle()
    {
        if (stateCurrent != State.Idle && stateCurrent != State.Walk)
        {
            ChangeState(State.Idle);
        }
    }

    void OnDrawGizmos()
    {
        // Set the line colour based on the current state
        switch (stateCurrent)
        {
            case State.Idle:
                Gizmos.color = Color.yellow;
                break;
            case State.Walk:
                Gizmos.color = Color.green;
                break;
            case State.Hunt:
                Gizmos.color = Color.red;
                break;
            case State.Flee:
                Gizmos.color = Color.magenta;
                break;
        }
        // Draw a line from the current position to our target position
        Gizmos.DrawLine(transform.position, currentTarget);
    }
}
