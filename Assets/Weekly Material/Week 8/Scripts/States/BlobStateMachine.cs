using UnityEngine;

public class BlobStateMachine : MonoBehaviour
{
    [SerializeField] private State[] states;
    [Tooltip("The closest another blob can get before they are noticed by this blob")]
    public float awarenessRange = 5f;

    [Tooltip("A directly reference to the player's blob size, so we can check how big they are compared to us")]
    public BlobSize playerBlob;

    /// <summary>
    /// The transform that the blob is currently focused on. This will be null if the blob is not focused on anything.
    /// Use this to track another blob this blob might need to know about, such as the player.
    /// </summary>
    public Transform focusedTransform;

    protected State stateCurrent;
    protected BlobSize mySize;

    public virtual void Start()
    {
        states = GetComponents<State>();
        mySize = GetComponent<BlobSize>();
        ChangeState(states[0].blobState);
    }

    public void Update()
    {
        CheckAwareness();
        stateCurrent.UpdateState();
    }

    public void ChangeState(BlobState newState)
    {
        if (stateCurrent.blobState == newState)
            return;
            
        foreach (State state in states)
        {
            if (state.blobState == newState)
            {
                stateCurrent = state;
                state.Enter();
                break;
            }
        }
    }

    /// <summary>
    /// Check if we should be aware of the player, and if so, whether we should hunt or flee from them. 
    /// </summary>
    protected virtual void CheckAwareness()
    {
        // If the player is within the range of awareness...
        if (IsInRange(playerBlob.transform, awarenessRange))
        {
            // If the player is a bigger blob...
            if (playerBlob.radius > mySize.radius)
            {
                // Run away
                ChangeState(BlobState.Flee);
            }
            else // Else, attack the player
            {
                ChangeState(BlobState.Hunt);
            }

            focusedTransform = playerBlob.transform;
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
}
