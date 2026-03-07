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
    private BlobSize mySize;

    /// <summary>
    /// The current smallest blob.
    /// </summary>
    private BlobSize smallestBlob;

    private List<BlobSize> biggerBlobs = new();

    void Start()
    {
        // Get the components
        trigger = GetComponent<CircleCollider2D>();

        // BlobSize component is on our parent object
        mySize = transform.parent.GetComponent<BlobSize>();

        // Ensure we have the correct awareness range
        Resize();
    }

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the thing that entered has a BlobSize component
        if (collision.GetComponent<BlobSize>())
        {
            BlobSize blob = collision.GetComponent<BlobSize>();
            
            // Add it to the list of blobs
            nearbyBlobs.Add(blob);
            CheckNewBlob(blob);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // We need to check if the component is there first
        if (collision.GetComponent<BlobSize>())
        {
            BlobSize blob = collision.GetComponent<BlobSize>();
            // Remove it from the list of blobs
            nearbyBlobs.Remove(blob);
            CheckRemovedBlob(blob);
        }
    }

    /// <summary>
    /// Use the current radius of the BlobSize component to resize the awareness radius. 
    /// </summary>
    public void Resize()
    {
        // Set the radius of our trigger based on how big our blob size is
        trigger.radius = mySize.radius * RadiusRatio;
    }

    /// <summary>
    /// Returns true if there is at least one blob within the awareness range.
    /// </summary>
    /// <returns></returns>
    public bool IsAnyoneNearby()
    {
        return nearbyBlobs.Count > 0;
    }

    /// <summary>
    /// If more than half the nearby blobs are larger than this blob, it is considered in danger.
    /// </summary>
    /// <returns></returns>
    public bool IsInDanger()
    {
        return biggerBlobs.Count > nearbyBlobs.Count / 2;
    }

    public Vector3 DirectionAwayFromDanger()
    {
        // To figure out the overall direction we want to go
        Vector3 directionAwayFromDanger = new();

        // Look at all the blobs in our awareness
        foreach (BlobSize currentBlob in biggerBlobs)
        {
            // Figure out the direction away from that blob
            Vector3 directionAway = transform.position - currentBlob.transform.position;

            //Add it to the overall direction I should move to avoid bigger blobs
            directionAwayFromDanger += directionAway.normalized;
        }

        // Normalise our flee direction
        return directionAwayFromDanger.normalized;
    }

    /// <summary>
    /// Get the current smallest blob.
    /// </summary>
    /// <returns></returns>
    public BlobSize GetSmallestBlob()
    {
        return smallestBlob;
    }

    /// <summary>
    /// Checks if a new blob is the smallest, or if it is bigger than this blob.
    /// </summary>
    /// <param name="blob"></param>
    void CheckNewBlob(BlobSize blob)
    {
        if (IsNewBiggerBlob(blob))
        {
            return;
        }

        CheckNewSmallestBlob(blob);
    }

    /// <summary>
    /// Returns whether or not the blob is bigger, adding to the bigger blob list if true.
    /// </summary>
    /// <param name="newBlob"></param>
    /// <returns></returns>
    private bool IsNewBiggerBlob(BlobSize newBlob)
    {
        if (newBlob.IsBiggerThan(mySize))
        {
            biggerBlobs.Add(newBlob);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Check if the blob is smaller than the current smallest blob, and update if so.
    /// </summary>
    /// <param name="newBlob"></param>
    void CheckNewSmallestBlob(BlobSize newBlob)
    {
        // If the blob is not smaller than us, it's not a valid choice
        if (newBlob.IsBiggerThan(mySize))
        {
            return;
        }

        // If we don't currently have a smallest blob...
        if (smallestBlob == null)
        {
            // now we do!
            smallestBlob = newBlob;
        }
        // Otherwise, if this blob is smaller than the current smallest blob...
        else if (newBlob.IsSmallerThan(smallestBlob))
        {
            // This blob is the new smallest blob
            smallestBlob = newBlob;
        }
    }

    /// <summary>
    /// Removes the blob from the bigger blob list if it was on it.
    /// Also checks if the blob was the smallest, and refreshes that information.
    /// </summary>
    /// <param name="blobRemoved"></param>
    void CheckRemovedBlob(BlobSize blobRemoved)
    {
        biggerBlobs.Remove(blobRemoved);

        // If it was the smallest blob...
        if (blobRemoved == smallestBlob)
        {
            // We need to update the smallest blob
            RefreshSmallestBlob();

        }
    }

    /// <summary>
    /// Update which blob in the nearby blobs is the smallest.
    /// </summary>
    void RefreshSmallestBlob()
    {
        // Start fresh
        smallestBlob = null;

        // Iterating through each blob nearby will find the smallest
        foreach (BlobSize blob in nearbyBlobs)
        {
            CheckNewSmallestBlob(blob);
        }
    }

}
