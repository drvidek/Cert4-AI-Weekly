using UnityEngine;

public class SmartBlobAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk,
        Hunt,
        Flee
    }

    public State stateCurrent;
    public float walkSpeed = 1.5f;
    public float huntSpeed = 3f;
    public float fleeSpeed = 2.5f;

    public float wanderRadius = 10f;
    public float awarenessRange = 5f;
    public float disengageRange = 7f;

    public float durationIdleMin = .5f;
    public float durationIdleMax = 1.5f;

    private float durationIdleCurrent;
    private BlobSize mySize;
    private Vector3 currentTarget;
    private BlobAwareness blobAwareness;

    void Start()
    {
        mySize = GetComponent<BlobSize>();
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
        // To figure out the overall direction we want to go
        Vector3 directionAwayFromDanger = new();

        if (!blobAwareness.IsAnyoneNearby())
        {
            ReturnToIdle();
            return;
        }

        // Track the overall threat of our surroundings
        int blobsBiggerThanMe = 0;

        // Track the smallest blob we've found
        BlobSize smallestBlob = null;

        foreach (var blob in blobAwareness.nearbyBlobs)
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
            // Normalise our direction
            directionAwayFromDanger.Normalize();

            currentTarget = transform.position + directionAwayFromDanger;

            // Run away from danger
            ChangeState(State.Flee);

        }
        else if (smallestBlob)
        {
            currentTarget = smallestBlob.transform.position;
            ChangeState(State.Hunt);
        }
        else
        {
            ReturnToIdle();
        }
    }

    // Safely return to the idle/walk loop without resetting it if it's currently active
    void ReturnToIdle()
    {
        if (stateCurrent != State.Idle && stateCurrent != State.Walk)
        {
            ChangeState(State.Idle);
        }
    }

    void OnDrawGizmos()
    {
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
        Gizmos.DrawLine(transform.position, currentTarget);
    }
}
