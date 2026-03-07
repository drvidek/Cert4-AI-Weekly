using UnityEngine;

public class LookTowardsTargetInRangeConstrained : LookTowardsTargetConstrained
{
    public float range = 5f;

    public override void LookTowards(Vector3 direction, float speed)
    {
        // If our target is not in range,
        if (!IsTargetInRange(range))
        {
            // Use our initial direction as the target direction
            direction = directionInitial;
        }

        // Use the inherited function
        base.LookTowards(direction, speed);
    }
}
