using UnityEngine;

public class FleeState : State
{
    public float speed = 2f;

    public float safeDistance = 5f;

    public override void Enter()
    {
        
    }

    public override void UpdateState()
    {
        // Get the direction away from the threat
        Vector3 directionAwayFromThreat = transform.position - stateMachine.focusedTransform.position;
        directionAwayFromThreat.Normalize();

        // Move in that direction
        Move(speed * Time.deltaTime * directionAwayFromThreat);

        // If the threat leaves the range
        if (!stateMachine.IsInRange(stateMachine.focusedTransform, safeDistance))
        {
            ChangeState(BlobState.Idle);
        }
    }
}
