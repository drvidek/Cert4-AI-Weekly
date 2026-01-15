using UnityEngine;

public class LookAtNearest : MonoBehaviour
{
    // A reference to our list of possible targets
    public TargetList targetList;

    // Refernce the target we should aim at currently
    public Transform targetCurrent;

    // Update is called once per frame
    void Update()
    {
        // If a target list has been assigned, and the list has targets at the moment...
        if (targetList && targetList.currentTargets.Count > 0)
        {
            // Set the current target based on which is nearest
            targetCurrent = FindNearestTarget();
            // Look at the target we found
            LookAt(targetCurrent.position);
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
    
    void LookAt(Vector3 point)
    {
        // Point the transform's 'forward' to directly face the given point
        transform.LookAt(point);

        // Point the transform's 'up' to that direction instead (because our turret points up)
        transform.up = transform.forward;
    }
}
