using UnityEngine;

public class FollowTargetInRange : FollowTarget
{
    public float range = 5f;
    
    // This is Polymorphism: keeping the method but defining new behaviour for it
    public override void SetDestination()   // 'override' allows us to create new behaviour in an inherited method
    {
        // If we have no target, or the target is not in range,
        if (!target || !IsTargetInRange(range))
        {
            // Cancel any active path
            agent.ResetPath();  // We can access 'agent' because it is protected, not private

            // Do nothing else
            return;
        }

        // Otherwise, use the behaviour from the base class (FollowTarget).
        base.SetDestination();
    }
}
