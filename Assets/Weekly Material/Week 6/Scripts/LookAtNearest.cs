using UnityEngine;

public class LookAtNearest : MonoBehaviour
{
    // A reference to our list of possible targets
    public TargetList targetList;

    // Refernce the target we should aim at currently
    public Transform targetCurrent;

    // Update is called once per frame
    void Update()
    {
        // If a target list has been assigned, and the list has targets at the moment...
        if (targetList && targetList.currentTargets.Count > 0)
        {
            // Set the current target based on which is nearest
            targetCurrent = targetList.FindNearestTarget();
            // Look at the target we found
            LookAt(targetCurrent.position);
        }
    }

   
    void LookAt(Vector3 point)
    {
        // Point the transform's 'forward' to directly face the given point
        transform.LookAt(point);

        // Point the transform's 'up' to that direction instead (because our turret points up)
        transform.up = transform.forward;
    }
}
