using UnityEngine;

public class RotateTowardsNearest : MonoBehaviour
{
    // A reference to our list of possible targets
    public TargetList targetList;

    // How quickly to rotate in degrees per second
    public float turnSpeed = 180;
    
    // The smallest angle allowed before we snap to face the target
    public float angleDifferenceMin = 1f;

    // Refernce the target we should aim at currently
    public Transform targetCurrent;

    void Update()
    {
        // If a target list has been assigned, and the list has targets at the moment...
        if (targetList && targetList.currentTargets.Count > 0)
        {
            // Set the current target based on which is nearest
            targetCurrent = FindNearestTarget();
            
            // If we somehow fail to find anything...
            if (!targetCurrent)
            {
                // Stop early for safety
                return;
            }

            // Direction from A to B is (B - A)
            Vector2 direction = targetCurrent.position - transform.position;

            // Normalise it
            direction.Normalize();

            // Get the direction we should rotate by checking the angle between our current direction (based on our up) and our desired direction
            // This number may be positive or negative depending on if we should turn clockwise or counter-clockwise
            float angleDifference = Vector2.SignedAngle(transform.up, direction);
            
            // If the difference between angles is smaller than our minimum...
            // (we use Mathf.Abs to make sure we have a positive number)
            if (Mathf.Abs(angleDifference) < angleDifferenceMin)
            {
                // Snap to the desired direction
                transform.up = direction;

                // Stop here
                return;
            }

            // Figure out whether we should go clockwise (positive) or counterclockwise (negative)
            // (we use Mathf.Sign to find whether a number is positive (1) or negative (-1))
            float angleDirection = Mathf.Sign(angleDifference);

            // Rotate around our Z axis, in the direction we determined, using the desired speed
            transform.Rotate(Vector3.forward, angleDirection * turnSpeed * Time.deltaTime);
        }
    }
    
    /// <summary>
    /// Checks the target list and returns the target which is nearest to the turret
    /// </summary>
    /// <returns></returns>
    private Transform FindNearestTarget()
    {
        // This will hold the distance between any given point and the turret
        float currentDistance;

        // This will track the shortest current distance between a target and the turret
        float minDistance = float.PositiveInfinity;

        // When we're done iterating, this will have a reference to the closest target.
        Transform currentTarget = null;

        // We will iterate through all the possible targets, and check how close each one is.
        // If we find that the current distance is shorter than our minimum distance,
        // we know that the target we're currently checking is a better choice.

        // For each target (which is the Transform type) in my list of possible targets...
        foreach (Transform target in targetList.currentTargets)
        {
            // Get the distance between the current target and the turret
            currentDistance = Vector3.Distance(target.position, transform.position);

            // If that distance is shorter than the current shortest distance...
            if (currentDistance < minDistance)
                // && IsTargetVisible(target))          /// Uncomment and fix the () pair to implement the sight-checking challenge
            {
                // We've found a new target
                currentTarget = target;
                // Update our minimum distance for future checks
                minDistance = currentDistance;
            }
        }

        // When done, we'll have figured out which target is the nearest
        return currentTarget;
    }

    // This is the optional solution to sight-checking.
    // This won't work without turning off "Queries Hit Triggers" in Project Settings > Physics2D
    // (or some LayerMask business which is out of scope for this lesson)
    private bool IsTargetVisible(Transform target)
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit.collider)
        {
            return hit.collider.CompareTag("Target");
        }
        return false;
    }
}
