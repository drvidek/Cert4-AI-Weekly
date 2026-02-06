using UnityEngine;

public class PivotTowardTarget : HasTarget
{
    public Vector3 axis = Vector3.up; 

    
    public void Update()
    {
        Quaternion rot = Quaternion.LookRotation(DirectionToTarget(), axis);
        transform.rotation = rot;
    }
}
