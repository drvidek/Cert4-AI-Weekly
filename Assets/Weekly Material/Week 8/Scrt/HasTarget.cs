using UnityEngine;

public class HasTarget : MonoBehaviour
{
    public Transform target;

    public Vector3 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
}
