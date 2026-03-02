using UnityEngine;

public class HuntState : State
{
    public float speed = 2f;

    public float disengageDistance = 7f;

    public override void Enter()
    {
        
    }

    public override void UpdateState()
    {
        // Get the direction towards the target
        Vector3 directionTowardsTarget = stateMachine.focusedTransform.position - transform.position;
        directionTowardsTarget.Normalize();

        Move(speed * Time.deltaTime * directionTowardsTarget);

        // If the threat leaves the range
        if (!stateMachine.IsInRange(stateMachine.focusedTransform, disengageDistance))
        {
            ChangeState(BlobState.Idle);
        }
    }

}
