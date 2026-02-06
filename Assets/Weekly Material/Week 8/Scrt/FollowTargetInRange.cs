using UnityEngine;

public class FollowTargetInRange : FollowTarget
{
    public float range = 5f;

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) >= range)
        {
            agent.ResetPath();
            return;
        }

        SetDestination();
    }
}
