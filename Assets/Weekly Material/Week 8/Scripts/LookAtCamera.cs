using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Reference to the main camera's transform
    private Transform cameraMain;

    private void Start()
    {
        // Get the main camera's transform at the start
        cameraMain = Camera.main.transform;
    }

    void Update()
    {
        // Make this object look at the camera every frame
        transform.forward = cameraMain.forward;
    }
}
