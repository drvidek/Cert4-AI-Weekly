using UnityEngine;

public class BlobCollision : MonoBehaviour
{
    // Get a reference to the BlobSize component
    public BlobSize mySize;

    void Start()
    {
        // Get the component off this game object
         mySize = GetComponent<BlobSize>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the thing we collided with also has a BlobSize component...
        if (collision.collider.TryGetComponent<BlobSize>(out BlobSize otherSize))
        {
            // If the other blob is smaller than me...
            if (otherSize.radius < mySize.radius)
            {
                // Increase my radius
                mySize.radius += otherSize.radius;

                // Grow bigger
                mySize.Resize();

                // Destroy the other blob
                Destroy(otherSize.transform.root.gameObject);
            }
        }
    }
}
