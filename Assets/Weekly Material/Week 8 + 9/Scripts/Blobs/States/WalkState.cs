using UnityEngine;

public class WalkState : BlobState
{
    public float speed = 1.5f;
    // The maximum distance to wander from your current location
    public float wanderRadius = 10f;

    // The current target for wandering
    private Vector3 currentTarget;

    public override string StateKey()
    {
        return "Walk";
    }

    public override void Enter()
    {
        // Get a random point inside a circle, where the circle centre is 0, 0
        Vector3 randomPoint = Random.insideUnitCircle;

        // Using the random point and our maximum wander radius, 
        // set a new target relative to our current position 
        currentTarget = stateMachine.transform.position + (wanderRadius * randomPoint);
    }

    public override void StateUpdate()
    {
        // Calculate and normalise the direction
        Vector3 direction = currentTarget - transform.position;
        direction.Normalize();

        // Move towards the current target
        Move(speed * Time.deltaTime * direction);

        if (Vector3.Distance(transform.position, currentTarget) <= 0.1f)
        {
            ChangeState("Idle");
        }
    }
}
