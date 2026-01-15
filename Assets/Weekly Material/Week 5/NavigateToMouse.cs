using UnityEngine;
using UnityEngine.AI;

public class NavigateToMouse : MonoBehaviour
{
    // Reference to the scene's camera
    private Camera mainCamera;

    // Reference to the NavMesh Agent component on this object
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Camera.main is convenient but expensive (it compares strings)
        // So we're just using it once, and storing the result in a variable
        mainCamera = Camera.main;

        // Get the NavMesh Agent component from this object
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the left mouse button is pressed...
        if (Input.GetMouseButtonDown(0))
        {
            // Create from the camera into the scene based on the mouse position
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // If a raycast using that ray hits something...
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Set the destination of our Nav Agent to the point that was clicked
                agent.SetDestination(hit.point);
            }
        }
    }
}
