using UnityEngine;

public class HuntState : BlobState
{
    public float speed = 2f;

    public float disengageDistance = 7f;

    public override void Enter()
    {
        
    }

    public override string StateKey()
    {
        return "Hunt";
    }

    public override void StateUpdate()
    {
        var stateMachine = this.stateMachine as BlobStateMachine;
        // If we ate our target already, or the hunt target is out of range...
        if (!stateMachine.focusedTransform || !stateMachine.IsInRange(stateMachine.focusedTransform, disengageDistance * stateMachine.GetBlobRadius()))
        {
            // Go back to Idle
            ChangeState("Idle");

            // Take no further action
            return;
        }

        // Get the direction towards the target
        Vector3 directionTowardsTarget = stateMachine.focusedTransform.position - transform.position;
        directionTowardsTarget.Normalize();

        Move(speed * Time.deltaTime * directionTowardsTarget);

    }

}
