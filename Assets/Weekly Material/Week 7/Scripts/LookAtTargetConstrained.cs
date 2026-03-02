using UnityEngine;

public class LookAtTargetConstrained : Week7.LookAtTarget
{
    public float angleMaxRotation = 45;

    private Vector3 initialDirection;

    void Start()
    {
        initialDirection = transform.forward;
    }

    new void Update()
    {
        if (!IsTargetInRange(3f))
        {
            transform.forward = initialDirection;
            return;
        }

        transform.forward = Vector3.RotateTowards(initialDirection, DirectionToTarget(), Mathf.Deg2Rad * angleMaxRotation, 0);
    }
}
