using UnityEngine.AI;

public class FollowTarget : HasTarget
{
    public NavMeshAgent agent;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        SetDestination();
    }

    public void SetDestination()
    {
        if (target)
        {
            agent.SetDestination(target.position);
        }
    }
}
