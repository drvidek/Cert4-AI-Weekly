using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;

    public float speed = 4f;

    public float minDistance = .1f;

    public Transform pointClosed;

    public Transform pointOpen;

    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isOpen)
        {
            transform.position = pointOpen.position;
        }
        else
        {
            transform.position = pointClosed.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            target = pointOpen;
        }
        else
        {
            target = pointClosed;
        }

        if (Vector3.Distance(transform.position, target.position) < minDistance)
        {
            transform.position = target.position;
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            transform.position += speed * Time.deltaTime * direction;
        }
    }
}
