using UnityEngine;
using UnityEngine.AI;

public class NavigateToObject : MonoBehaviour
{
    // The object to move towards
    public Transform target;
    
    // A reference to the NavAgent component on the game object.
    public NavMeshAgent agent;

    void Start()
    {
        // Get the NavMeshAgent component on this game object
        agent = GetComponent<NavMeshAgent>();

        // Set the agent's destination using the target object
        agent.SetDestination(target.position);
    }

}
