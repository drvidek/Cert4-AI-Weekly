using UnityEngine;

public class FleeState : BlobState
{
    public float speed = 2f;

    public override void Enter()
    {

    }

    public override string StateKey()
    {
        return "Flee";
    }

    public override void StateUpdate()
    {
        var blob = stateMachine as BlobStateMachine;
        // Move in the direction away from the threat
        Move(speed * Time.deltaTime * blob.DirectionAwayFromDanger());
        if (!blob.IsInDanger())
        {
            ChangeState("Idle");
        }
    }
}
