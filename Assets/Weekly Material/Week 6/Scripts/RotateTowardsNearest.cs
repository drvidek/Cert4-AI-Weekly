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
            targetCurrent = targetList.FindNearestTarget();
            
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
}
