using System.Collections.Generic;
using UnityEngine;

public class TargetList : MonoBehaviour
{
    // Keep track of all the targets in range to shoot at
    public List<Transform> currentTargets = new List<Transform>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the thing that entered is a valid target...
        if (collision.gameObject.CompareTag("Target"))
        {
            // Add it to the list of targets
            currentTargets.Add(collision.transform);
        }
    }
    /// <summary>
    /// Checks the target list and returns the target which is nearest to the turret
    /// </summary>
    /// <returns></returns>
    public Transform FindNearestTarget()
    {
        // This will hold the distance between any given point and the turret
        float currentTargetDistance;

        // This will track the shortest current distance between a target and the turret
        float minDistance = float.PositiveInfinity;

        // When we're done iterating, this will have a reference to the closest target.
        Transform currentTarget = null;

        // We will iterate through all the possible targets, and check how close each one is.
        // If we find that the current distance is shorter than our minimum distance,
        // we know that the target we're currently checking is a better choice.

        // For each target (which is the Transform type) in my list of possible targets...
        foreach (Transform target in currentTargets)
        {
            // Get the distance between the current target and the turret
            currentTargetDistance = Vector3.Distance(target.position, transform.position);

            // If that distance is shorter than the current shortest distance...
            if (currentTargetDistance < minDistance)
            // && IsTargetVisible(target))          /// Uncomment and fix the () pair to implement the sight-checking challenge
            {
                // We've found a new target
                currentTarget = target;
                // Update our minimum distance for future checks
                minDistance = currentTargetDistance;
            }
        }

        // When done, we'll have figured out which target is the nearest
        return currentTarget;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        // We can safely remove from a list even if the item isn't actually on it
        currentTargets.Remove(collision.transform);
    }
}
