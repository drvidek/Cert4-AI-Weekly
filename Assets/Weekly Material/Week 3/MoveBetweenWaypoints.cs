using UnityEngine;

public class MoveBetweenWaypoints : MonoBehaviour
{
    // This array will hold a reference to each of the waypoints this object should move between.
    public Transform[] waypoints;

    // How fast the object will move, in units per second
    public float speed = 3; 

    // How close we need to get to the waypoint before we're "there"
    public float minimumDistance = 0.1f;

    //// The below variables are public only so we can see things working ////
    // Which waypoint we're currently moving towards
    public int index;

    // The current direction 
    public Vector3 direction;

    void Start()
    {
        // Figure out our initial direction
        CalculateDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the current waypoint 
        transform.position += speed * Time.deltaTime * direction;

        // If the distance from the object's position to the current waypoint is less than our forgiveness
        if (Vector3.Distance(transform.position, waypoints[index].position) <= minimumDistance)
        {
            // Snap to the position to properly finish the path
            transform.position = waypoints[index].position;

            // Increase the waypoint index (so we move to the next waypoint)
            index++;    // same as 'index += 1' and 'index = index + 1'

            // If the waypoint index reaches the end of the array...
            if (index >= waypoints.Length)
            {
                // Loop back to the start
                index = 0;
            }

            // Figure out our new direction
            CalculateDirection();
        }
    }

    /// <summary>
    /// Determine the direction from our current position to the target.
    /// </summary>
    void CalculateDirection()
    {
        // Get the direction towards the target - the direction from A to B is always (B - A)
        direction = waypoints[index].position - transform.position;

        // Normalise the direction to one unit long
        direction.Normalize();
    }
}
