using UnityEngine;
using System.Collections.Generic;

public class BlobAwareness : MonoBehaviour
{
    public const float RadiusRatio = 5f;
 
    // Keep track of all the blobs in range
    public List<BlobSize> nearbyBlobs = new List<BlobSize>();

    private CircleCollider2D trigger;
    private BlobSize blobSize;

    void Start()
    {
        trigger = GetComponent<CircleCollider2D>();
        blobSize = transform.parent.GetComponent<BlobSize>();
    }

    void Update()
    {
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

    public bool IsAnyoneNearby()
    {
        return nearbyBlobs.Count > 0;
    }
}
