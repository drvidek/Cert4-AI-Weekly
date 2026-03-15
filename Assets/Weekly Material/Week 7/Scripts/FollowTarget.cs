using UnityEngine.AI;

public class FollowTarget : HasTarget
{
    // protected is like private, but inheriting scripts can see it
    protected NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Update our target destination every step.
        SetDestination();
    }

    ////// TEACHER! Do not use the virtual modifier at first
    /// Come back here when making 'FollowTargetInRange'

    /// <summary>
    /// If there is a target, set the destination to that target.
    /// </summary>
    public virtual void SetDestination()        // 'virtual' allows us to override this function with new behaviour 
    {
        if (target)
        {
            agent.SetDestination(target.position);
        }
    }
}
