using UnityEngine;

public class BlobStateMachine : StateMachine
{
    protected BlobAwareness blobAwareness;

    [Tooltip("The closest another blob can get before they are noticed by this blob")]
    public float awarenessRange = 5f;

    /// <summary>
    /// The transform that the blob is currently focused on. This will be null if the blob is not focused on anything.
    /// Use this to track another blob this blob might need to know about, such as the player.
    /// </summary>
    public Transform focusedTransform;

    protected BlobSize mySize;

    public override void Start()
    {
        blobAwareness = GetComponentInChildren<BlobAwareness>();
        mySize = GetComponent<BlobSize>();
        base.Start();
    }

    public override void Update()
    {
        CheckAwareness();

        base.Update();
    }

    /// <summary>
    /// Checks if the blob is currently in danger, according to its awareness.
    /// </summary>
    /// <returns></returns>
    public bool IsInDanger()
    {
        return blobAwareness.IsInDanger();
    }

    /// <summary>
    /// Get the direction away from danger via the blob's awareness.
    /// </summary>
    /// <returns></returns>
    public Vector3 DirectionAwayFromDanger()
    {
        return blobAwareness.DirectionAwayFromDanger();
    }

    // Give this state machine a completely different means of checking awareness
    // (this is polymorphism)
    protected void CheckAwareness()
    {
        // If there are more than 50% bigger blobs nearby...
        if (IsInDanger())
        {
            // Run away
            ChangeState("Flee");
        }
        // Else, if we found a smallest blob
        else if (blobAwareness.GetSmallestBlob())
        {
            // Target that blob
            focusedTransform = blobAwareness.GetSmallestBlob().transform;

            // Change to hunting
            ChangeState("Hunt");
        }
    }

    /// <summary>
    /// Returns true if the distance from this blob to the given target is within the given distance, else returns false
    /// </summary>
    /// <param name="distance"></param>
    /// <returns></returns>
    public bool IsInRange(Transform target, float distance)
    {
        return Vector3.Distance(transform.position, target.position) <= distance;
    }

    public float GetBlobRadius()
    {
        return mySize.radius;
    }
}
