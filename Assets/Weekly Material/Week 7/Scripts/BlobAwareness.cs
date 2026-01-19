using UnityEngine;
using System.Collections.Generic;

public class BlobAwareness : MonoBehaviour
{
    // When the Blob has a size of 1, this is how big the radius of the awareness trigger will be
    public const float RadiusRatio = 5f;
 
    // Keep track of all the blobs in range
    public List<BlobSize> nearbyBlobs = new List<BlobSize>();
    
    // Reference to our awareness trigger
    private CircleCollider2D trigger;
    
    // Reference to our blob size
    private BlobSize blobSize;

    void Start()
    {
        // Get the components
        trigger = GetComponent<CircleCollider2D>();

        // BlobSize component is on our parent object
        blobSize = transform.parent.GetComponent<BlobSize>();
    }

    void Update()
    {
        // Set the radius of our trigger based on how big our blob size is
        // (this is not very efficient to do every frame but works fine for now)
        trigger.radius = blobSize.radius * RadiusRatio;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the thing that entered has a BlobSize component
        if (collision.GetComponent<BlobSize>())
        {
            // Add it to the list of blobs
            nearbyBlobs.Add(collision.GetComponent<BlobSize>());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // We need to check if the component is there first
        if (collision.GetComponent<BlobSize>())
        {
            // Remove it from the list of blobs
            nearbyBlobs.Remove(collision.GetComponent<BlobSize>());
        }
    }

    /// <summary>
    /// Returns true if there is at least one blob within the awareness range.
    /// </summary>
    /// <returns></returns>
    public bool IsAnyoneNearby()
    {
        return nearbyBlobs.Count > 0;
    }
}
