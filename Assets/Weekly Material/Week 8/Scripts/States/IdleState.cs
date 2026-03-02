using UnityEngine;

public class IdleState : State
{
    public float durationIdleMin = .5f;
    // The longest the blob can wait during Idle state
    public float durationIdleMax = 1.5f;
    // How long the blob currently has left to wait in an Idle state
    private float durationIdleCurrent;

    public override void Enter()
    {
        // Set our current idle duration to a random value between our min and max
        durationIdleCurrent = Random.Range(durationIdleMin, durationIdleMax);
    }

    public override void UpdateState()
    {
        // Count down our idle time towards 0
        durationIdleCurrent -= Time.deltaTime;

        // Once we reach 0...
        if (durationIdleCurrent <= 0)
        {
            // Change to the Walk state
            ChangeState(BlobState.Walk);
        }
    }

}
