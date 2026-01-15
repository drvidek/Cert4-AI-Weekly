using UnityEngine;

public class LookAtFirstTarget : MonoBehaviour
{
    // A reference to our list of possible targets
    public TargetList targetList;

    // Update is called once per frame
    void Update()
    {
        // If a target list has been assigned, and the list has targets at the moment...
        if (targetList && targetList.currentTargets.Count > 0)
        {
            // Look at the first target on the list
            LookAt(targetList.currentTargets[0].position);
        }
    }
    
    /// <summary>
    /// Turn the object to directly face the target.
    /// </summary>
    /// <param name="point"></param>
    void LookAt(Vector3 point)
    {
        // Point the transform's 'forward' to directly face the given point
        transform.LookAt(point);

        // Point the transform's 'up' to that direction instead (because our turret points up)
        transform.up = transform.forward;
    }
}
