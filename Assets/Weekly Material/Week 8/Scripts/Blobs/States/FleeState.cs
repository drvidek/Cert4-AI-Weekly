using UnityEngine;

public class FleeState : StateBehaviour
{
    public float speed = 2f;

    public override void Enter()
    {

    }

    public override void StateUpdate()
    {
        // Move in the direction away from the threat
        Move(speed * Time.deltaTime * stateMachine.DirectionAwayFromDanger());

        // If the threat leaves the range
        if (!stateMachine.IsInDanger())
        {
            // Stop hunting
            ChangeState(BlobState.Idle);
        }
    }
}
