using UnityEngine;

public class InteractToFollow : Interaction
{
    private FollowTarget follow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        follow = GetComponent<FollowTarget>();
    }

    public override void Interact(GameObject other)
    {
        // If we have a FollowTarget component...
        if (follow)
        {
            // If that component has no target...
            if (!follow.HasTarget())
            {
                // Use the other transform to set a new target
                follow.SetTarget(other.transform);
            }
            else
            {
                // Remove the target
                follow.SetTarget(null);
            }
        }
    }
}
