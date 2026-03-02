using UnityEngine;

public class FlatLookAtTarget : Week7.LookAtTarget
{
    [Tooltip("The axis around which to pivot. Up is default.")]
    [SerializeField] private Vector3 axis = Vector3.up;

    // Update is called once per frame
    new void Update()
    {
        LookAt();
        FlattenToAxis();
    }

    public void FlattenToAxis()
    {
        SetAngles(Vector3.Scale(transform.localEulerAngles, axis));
    }
}
