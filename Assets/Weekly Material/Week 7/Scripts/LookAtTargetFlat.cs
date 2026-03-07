using UnityEngine;

public class LookAtTargetFlat : Week7.LookAtTarget
{
    [Tooltip("The axis around which to pivot. Up is default.")]
    [SerializeField] private Vector3 axis = Vector3.up;

    // Again, Polymorphism
    // Using the same method but providing new behaviours
    override public void LookAt()
    {
        // Use the base behaviour to look directly at the target
        base.LookAt();

        // .Scale() multiplies each vector's x, y, and z with each other (x * x, y * y, z * z)
        // Scaling by an axis of (0, 1, 0) will result in only horizontal rotation
        Vector3 flattenedRotation = Vector3.Scale(transform.localEulerAngles, axis);

        // Set the angles of the object using our flattened rotation
        transform.localEulerAngles = flattenedRotation;
    }
}
