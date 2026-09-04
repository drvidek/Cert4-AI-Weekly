using UnityEngine;

public class LookTowardsTargetConstrained : LookTowardsTarget
{

    [Tooltip("The maximum angle to rotate away from the initial direction")]
    public float angleConstraint = 45;

    [Range(0, 1)]
    public float verticalConstraint = .5f;

    // Hold the direction this object started out at
    protected Vector3 directionInitial;

    void Start()
    {
        // Copy the direction we are looking at to begin with
        directionInitial = transform.forward;
    }

    public override void LookTowards(Vector3 direction, float speed)
    {
        Vector3 relativeInitial = directionInitial;

        // If we have a parent transform, we should use it's direction as a reference
        if (transform.parent)
        {
            relativeInitial = transform.parent.TransformDirection(directionInitial);
        }

        // By first rotating from our initial direction towards our target direction,
        // limiting by our maximum rotation angle,
        // we determine the direction we're allowed to rotate towards
        Vector3 directionPossible = Vector3.RotateTowards(relativeInitial, direction, Mathf.Deg2Rad * angleConstraint, 0);

        // Now we can use this new direction with the default behaviour
        base.LookTowards(directionPossible, speed);
    }
}
