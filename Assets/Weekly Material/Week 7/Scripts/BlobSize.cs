using UnityEngine;

public class BlobSize : MonoBehaviour
{
    // How big this blob's radius should be
    public float radius = .5f;

    // Whether or not to start with a randomised radius
    public bool randomRadius;

    // Reference to this blob's collider
    private CircleCollider2D collider;

    // Reference to this blob's sprite
    private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Randomise the radius if the box has been ticked
        if (randomRadius)
        {
            // Use a number between 0.2 and 1.2 as the new radius
            radius = Random.Range(0.2f, 1.2f);
        }
        // Set our size based on the radius
        Resize();
    }

    void Secure()
    {
        // If we have no reference to the collider...
        if (!collider)
        {
            // Get the reference
            collider = GetComponent<CircleCollider2D>();
        }

        // Same with the sprite renderer
        if (!sprite)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    void OnValidate()
    {
        Resize();
    }

    public void Resize()
    {
        // Make sure we have the components we need
        Secure();

        // Update the collider's radius
        collider.radius = radius;

        // Set the size of the sprite to match the size of the collider
        sprite.size = radius * 2f * Vector2.one;
    }
}
