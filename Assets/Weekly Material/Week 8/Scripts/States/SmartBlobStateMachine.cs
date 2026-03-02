using UnityEngine;

public class SmartBlobStateMachine : BlobStateMachine
{
    protected BlobAwareness blobAwareness;

    protected Vector3 fleeDirection;

    public override void Start()
    {
        base.Start();
        blobAwareness = GetComponentInChildren<BlobAwareness>();
    }

    override protected void CheckAwareness()
    {
        // If no one is nearby...
        if (!blobAwareness.IsAnyoneNearby())
        {
            // Just go back to Idle/Walk
            ChangeState(BlobState.Walk);

            // Don't do anything else in this function
            return;
        }

        // To figure out the overall direction we want to go
        Vector3 directionAwayFromDanger = new();

        // Track the overall threat of our surroundings
        int blobsBiggerThanMe = 0;

        // Track the smallest blob we've found
        BlobSize smallestBlob = null;

        // Look at all the blobs in our awareness range
        foreach (BlobSize blob in blobAwareness.nearbyBlobs)
        {
            // If the blob is bigger than me
            if (blob.radius > mySize.radius)
            {
                // Count up how many blobs are bigger than me
                blobsBiggerThanMe++;

                // Figure out the direction away from that blob
                Vector3 directionAway = transform.position - blob.transform.position;

                //Add it to the overall direction I should move to avoid bigger blobs
                directionAwayFromDanger += directionAway.normalized;
            }

            // If the blob is smaller than me...
            else if (blob.radius < mySize.radius)
            {
                // If we don't currently have a smallest blob...
                if (smallestBlob == null)
                {
                    // now we do!
                    smallestBlob = blob;
                }
                // Otherwise, if this blob is smaller than the current smallest blob...
                else if (blob.radius < smallestBlob.radius)
                {
                    // This blob is the new smallest blob
                    smallestBlob = blob;
                }
            }
        }

        // If there are more than 50% bigger blobs...
        if (blobsBiggerThanMe >= blobAwareness.nearbyBlobs.Count / 2f)
        {
            if (directionAwayFromDanger.magnitude < 1f)
            {
                directionAwayFromDanger = Random.insideUnitCircle.normalized;
            }

            // Normalise our flee direction
            directionAwayFromDanger.Normalize();

            // Figure out a "target" by adding the direction to our current position
            // We're multiplying by 5f for Gizmo drawing purposes but just adding the direction would work totally fine
            fleeDirection = transform.position + (directionAwayFromDanger * 5f);

            // Run away
            ChangeState(BlobState.Flee);

        }
        // Else if we found a smaller blob
        else if (smallestBlob)
        {
            // Target that blob
            focusedTransform = smallestBlob.transform;

            // Change to hunting
            ChangeState(BlobState.Hunt);
        }
        else
        {
            // Else, we should go back to Idle/Walk
            ChangeState(BlobState.Idle);
        }
    }
}
