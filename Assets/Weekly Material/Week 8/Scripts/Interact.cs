using UnityEngine;

public class Interact : MonoBehaviour
{
    private Camera cameraMain;
    void Start()
    {
        // Get and store a reference to the main camera
        cameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            // Attempt to interact with an object
            TryToInteract();
        }
    }

    void TryToInteract()
    {
        // Create a ray from the camera through the mouse position
        Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

        // Perform the raycast
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Check if the hit object has a rigidbody, and an NPCDialogue component
            if (hitInfo.rigidbody && hitInfo.rigidbody.TryGetComponent<NPCDialogue>(out NPCDialogue dialogue))
            {
                // Show the dialogue from the NPCDialogue component
                dialogue.ShowDialogue();
            }
        }
    }
}
