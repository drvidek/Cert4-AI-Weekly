using UnityEngine;

/// <summary>
/// A component which provides the framework for game behaviours that require a target object,
/// such as looking or aiming at something, or following something.
/// </summary>
public class TargetUser : MonoBehaviour
{
    [Tooltip("The transform considered the target of this component")]
    [SerializeField] protected Transform target;

    /// <summary>
    /// Get the direction to the target as a normalized vector.
    /// </summary>
    /// <returns>The direction to the target, normalised.</returns>
    public Vector3 DirectionToTarget()
    {
        // This is Abstraction:
        // The method provides a way to get the direction to the target,
        // without needing to know how it's calculated.
        return (target.position - transform.position).normalized;
    }

    /// <summary>
    /// Set the target for this object.
    /// </summary>
    /// <param name="newTarget">The transform to consider the target. Can be null.</param>
    public void SetTarget(Transform newTarget)
    {
        // This is Encapsulation:
        // We provide a controlled way to set a new target,
        // without exposing the target transform with a public variable
        target = newTarget;
    }

    /// <summary>
    /// Check if the target is already set.
    /// </summary>
    /// <returns>True if a target is set, else false</returns>
    public bool HasTarget()
    {
        // This is also Abstraction:
        // The method provides a way to check if the target is set,
        // without needing to know how the target is stored or managed.
        return target != null;
    }

    /// <summary>
    /// Check if the target is within a given range.
    /// </summary>
    /// <param name="range">The maximum range to check the target is within.</param>
    /// <returns>True if the target is within range, else false</returns>
    public bool IsTargetInRange(float range)
    {
        // More abstraction!
        // This helps us check if the target object is in range,
        // without needing to know the specific calculations for this
        return Vector3.Distance(transform.position, target.position) <= range;
    }
}
