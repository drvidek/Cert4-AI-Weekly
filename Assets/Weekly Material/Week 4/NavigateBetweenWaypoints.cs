using UnityEngine;
using UnityEngine.AI;

public class NavigateBetweenWaypoints : MonoBehaviour
{
    // The object to move towards
    public Transform[] waypoints;
    
    // A reference to the NavAgent component on the game object.
    public NavMeshAgent agent;

    // This is now private because we've seen it work before, and it shouldn't be meddled with by designers
    private int index;

    void Start()
    {
        // Get the NavMeshAgent component on this game object
        agent = GetComponent<NavMeshAgent>();

        // Set the agent's destination using the target object
        agent.SetDestination(waypoints[index].position);
    }

    void Update()
    {
        if (IsDesitnationReached())
        {
            index++;

            if (index >= waypoints.Length)
            {
                index = 0;
            }

            agent.SetDestination(waypoints[index].position);
        }
    }

    /// <summary>
    /// Returns true or false depending on if the Agent has reached the current destination
    /// </summary>
    /// <returns></returns>
    bool IsDesitnationReached()
    {
        // If the path has been calculated (i.e. not pending),
        return !agent.pathPending &&
        // and the distance to the destination is less than the stopping distance,
                agent.remainingDistance <= agent.stoppingDistance;

        // We know have reached our destination
    }
}
