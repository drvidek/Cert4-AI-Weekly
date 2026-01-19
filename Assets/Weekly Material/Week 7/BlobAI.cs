using UnityEngine;
using UnityEngine.AI;

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

    public float wanderRange = 10f;
    public float awarenessRange = 5f;
    public float safetyRange = 7f;

    public float durationIdleMin = .5f;
    public float durationIdleMax = 1.5f;

    public BlobSize player;

    public float durationIdleCurrent;

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
        MoveTowards(player.transform.position, huntSpeed);
        if (!IsPlayerInRange(safetyRange))
        {
            SetRandomDuration();
            ChangeState(State.Idle);
        }
    }

    void Flee()
    {
        // A bit cheeky, but using a negative speed here will make us move away instead of towards
        MoveTowards(player.transform.position, -fleeSpeed);
        if (!IsPlayerInRange(safetyRange))
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
        currentTarget = wanderRange * randomPoint;
    }

    bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    void CheckForPlayerNearby()
    {
        if (IsPlayerInRange(awarenessRange))
        {
            if (player.radius > mySize.radius)
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
