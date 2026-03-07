using UnityEngine;

public class LookTowardsTarget : HasTarget
{
    [Tooltip("How fast to look towards the target, in degrees per second")]
    public float lookSpeed = 180;

    void Update()
    {
        // Use the direction to target to look towards
        LookTowards(DirectionToTarget(), lookSpeed);
    }

    /// <summary>
    /// Rotate towards the given direction in the given speed at degrees per second.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="speed"></param>
    public virtual void LookTowards(Vector3 direction, float speed)
    {
        // Rotate our forward vector towards the desired direction, in 'speed' degrees per second
        transform.forward = Vector3.RotateTowards(transform.forward, direction, Mathf.Deg2Rad * speed * Time.deltaTime, 0);
    }
}
