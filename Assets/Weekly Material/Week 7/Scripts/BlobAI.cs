using UnityEngine;

public class BlobAI : MonoBehaviour
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
    private BlobSize playerBlob;

    private BlobSize mySize;

    private Vector3 currentTarget;

    void Start()
    {
        mySize = GetComponent<BlobSize>();
        SetRandomTarget();
    }

    void Update()
    {
        CheckForPlayerNearby();
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
        MoveTowards(playerBlob.transform.position, huntSpeed);
        if (!IsPlayerInRange(disengageRange))
        {
            SetRandomDuration();
            ChangeState(State.Idle);
        }
    }

    void Flee()
    {
        // A bit cheeky, but using a negative speed here will make us move away instead of towards
        MoveTowards(playerBlob.transform.position, -fleeSpeed);

        if (!IsPlayerInRange(disengageRange))
        {
            SetRandomDuration();
            ChangeState(State.Idle);
        }
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

    bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, playerBlob.transform.position) <= range;
    }

    void CheckForPlayerNearby()
    {
        if (IsPlayerInRange(awarenessRange))
        {
            if (playerBlob.radius > mySize.radius)
            {
                ChangeState(State.Flee);
            }
            else
            {
                ChangeState(State.Hunt);
            }
        }
    }
}
