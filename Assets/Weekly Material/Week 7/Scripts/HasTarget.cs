using UnityEngine;

public class HasTarget : MonoBehaviour
{
    // This is Encapsulation:
    // The variable is protected and can only be accessed through methods.
    [SerializeField] protected Transform target;

    /// <summary>
    /// Get the direction to the target as a normalized vector.
    /// </summary>
    /// <returns></returns>
    public Vector3 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
        // This is Abstraction:
        // The method provides a way to get the direction to the target,
        // without needing to know how it's calculated.
    }

    /// <summary>
    /// Set the target for this object.
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    /// <summary>
    /// Check if the target is already set.
    /// </summary>
    /// <returns></returns>
    public bool IsTargetSet()
    {
        return target != null;
        // This is also Abstraction:
        // The method provides a way to check if the target is set,
        // without needing to know how the target is stored or managed.
    }

    /// <summary>
    /// Check if the target is within a given range.
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool IsTargetInRange(float range)
    {
        return Vector3.Distance(transform.position, target.position) <= range;
    }
}
