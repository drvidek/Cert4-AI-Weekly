using UnityEngine;

public class MoveTowardsObject : MonoBehaviour
{
    // The object to move towards
    public Transform target;

    // How fast the object will move, in units per second
    public float speed = 3;

    // How close we need to get to the waypoint before we're "there"
    public float minimumDistance = 0.1f;
    // The current direction 
    public Vector3 direction;

    void Start()
    {
        CalculateDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // If the distance from the object's position to the current target is less than the minimum distance...
        // aka if we have arrived at our target...
        if (Vector3.Distance(transform.position, target.position) <= minimumDistance)
        {
            // Snap to the position to properly finish the path
            transform.position = target.position;
        }
        else    // If we haven't arrived yet...
        {
            // Move towards the target 
            transform.position += speed * Time.deltaTime * direction;
        }
    }

    /// <summary>
    /// Determine the direction from our current position to the target.
    /// </summary>
    void CalculateDirection()
    {
        // Get the direction towards the target - the direction from A to B is always (B - A)
        direction = target.position - transform.position;

        // Normalise the direction to one unit long
        direction.Normalize();
    }
}
