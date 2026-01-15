using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    // The target to look at
    public Transform target;
   
    // Update is called once per frame
    void Update()
    {
        // First version. Look directly at the target.
        LookAt(target.position);
    }

    void LookAt(Vector3 point)
    {
        // Point the transform's 'forward' to directly face the given point
        transform.LookAt(point);

        // Point the transform's 'up' to that direction instead (because our turret points up)
        transform.up = transform.forward;
    }
}
