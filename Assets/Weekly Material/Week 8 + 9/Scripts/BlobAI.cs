using UnityEngine;

public class BlobAI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Roam,
        Hunt,
        Flee
    }

    public State state;

    [Tooltip("The closest another blob can get before they are noticed by this blob")]
    public float awarenessRange = 5f;

    [Tooltip("Extra distance required to break awareness")]
    public float rangeBuffer = 2f;

    [Tooltip("A direct reference to the player's blob size, so we can check how big they are compared to us")]
    public BlobSize playerBlob;

    [Tooltip("How fast the blob can move")]
    public float speed;

    [Tooltip("The maximum distance from its starting point the blob can roam")]
    public float roamRange = 5f;

    [Tooltip("The distance from the target location at which roaming is complete")]
    public float targetDistance = 0.1f;


    private BlobSize mySize;

    private float delay;

    private Vector3 target;

    private bool hasEnteredState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mySize = GetComponent<BlobSize>();
        ChangeState(State.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAwareness();

        switch (state)
        {
            case State.Idle:
                delay -= Time.deltaTime;
                if (delay <= 0f)
                {
                    ChangeState(State.Roam);
                }
                break;
            case State.Roam:
                Move(DirectionTo(target));
                if (Vector3.Distance(transform.position, target) <= targetDistance)
                {
                    ChangeState(State.Idle);
                }
                break;
            case State.Hunt:
                Move(DirectionTo(playerBlob.transform.position));
                break;
            case State.Flee:
                Move(DirectionAwayFrom(playerBlob.transform.position));
                break;
        }
    }

    public void ChangeState(State state)
    {
        if (hasEnteredState && this.state == state)
        {
            return;
        }

        this.state = state;
        hasEnteredState = true;

        EnterState(state);
    }

    private void EnterState(State state)
    {
        switch (state)
        {
            case State.Idle:
                delay = Random.Range(1f, 2f);
                break;
            case State.Roam:
                Vector2 randomOffset = Random.insideUnitCircle * roamRange;
                target = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
                break;
            case State.Hunt:
            case State.Flee:
                break;
        }
    }

    private void Move(Vector3 movement)
    {
        transform.position += speed * Time.deltaTime * movement;
    }

    private Vector3 DirectionTo(Vector3 target)
    {
        return (target - transform.position).normalized;
    }

    private Vector3 DirectionAwayFrom(Vector3 target)
    {
        return -DirectionTo(target);
    }

    private void UpdateAwareness()
    {
        if (!playerBlob)
        {
            return;
        }

        float distance = Vector3.Distance(transform.position, playerBlob.transform.position);

        if (state == State.Hunt || state == State.Flee)
        {
            bool playerIsNearby = distance <= awarenessRange + rangeBuffer;

            if (!playerIsNearby)
            {
                ChangeState(State.Idle);
            }
            return;
        }

        if (distance <= awarenessRange)
        {
            if (playerBlob.IsBiggerThan(mySize))
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
