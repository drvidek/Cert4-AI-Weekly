using UnityEngine;

public class FollowTargetInRange : FollowTarget
{
    public float range = 5f;

    void Update()
    {
        if (!IsTargetInRange(range))
        {
            agent.ResetPath();
            return;
        }

        SetDestination();
    }
}
