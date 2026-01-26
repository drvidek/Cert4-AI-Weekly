using UnityEngine;

public class Player3D : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get and store a reference to the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Ensure the player is not frozen at the start
        SetFreeze(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from the horizontal and vertical axes
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Create a movement vector based on the input
        Vector3 movement = new Vector3(input.x, 0, input.y);
        // Preserve the existing vertical velocity (y-axis)
        float previousY = rb.linearVelocity.y;
        // Set the Rigidbody's velocity to move the player including the preserved vertical velocity
        rb.linearVelocity = movement * speed + Vector3.up * previousY;
    }

    // Freezes or unfreezes the player's movement
    public void SetFreeze(bool freeze)
    {
        // Set Rigidbody constraints based on the freeze parameter
        rb.constraints = freeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.FreezeRotation;

        // RigidbodyConstraints.FreezeAll will prevent any physics movement or rotation
        // This counts for physics applied through code, like we have in Update
        // (you can still move and rotate the Transform directly)
    }
}
