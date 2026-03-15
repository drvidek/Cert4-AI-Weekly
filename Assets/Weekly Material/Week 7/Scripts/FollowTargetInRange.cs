using UnityEngine;

public class FollowTargetInRange : FollowTarget
{
    public float range = 5f;
    
    ////// TEACHER! This may be the learner's first time using virtual/override
    // Discuss here how we want the base behaviour from FollowTarget,
    // BUT we want to add a range factor to that behaviour...
    // Instead of writing a brand new method, we can use Polymorphism!
    // Go back to FollowTarget and implement the 'virtual' keyword

    // This is Polymorphism: keeping the method as named, but defining new behaviour for it
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
