using UnityEngine;

public class SmartBlobStateMachine : BlobStateMachine
{
    protected BlobAwareness blobAwareness;

    public override void Start()
    {
        blobAwareness = GetComponentInChildren<BlobAwareness>();
        base.Start();
    }

    /// <summary>
    /// Checks if the blob is currently in danger, according to its awareness.
    /// </summary>
    /// <returns></returns>
    override public bool IsInDanger()
    {
        return blobAwareness.IsInDanger();
    }

    /// <summary>
    /// Get the direction away from danger via the blob's awareness.
    /// </summary>
    /// <returns></returns>
    override public Vector3 DirectionAwayFromDanger()
    {
        return blobAwareness.DirectionAwayFromDanger();
    }
    
    // Give this state machine a completely different means of checking awareness
    // (this is polymorphism)
    override protected void CheckAwareness()
    {
        // If there are more than 50% bigger blobs nearby...
        if (IsInDanger())
        {
            // Run away
            ChangeState(BlobState.Flee);
        }
        // Else, if we found a smallest blob
        else if (blobAwareness.GetSmallestBlob())
        {
            // Target that blob
            focusedTransform = blobAwareness.GetSmallestBlob().transform;

            // Change to hunting
            ChangeState(BlobState.Hunt);
        }
    }
}
