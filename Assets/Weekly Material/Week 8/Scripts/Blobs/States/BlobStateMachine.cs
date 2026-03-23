using UnityEngine;

public class BlobStateMachine : MonoBehaviour
{
    [SerializeField] private StateBehaviour[] states;
    [Tooltip("The closest another blob can get before they are noticed by this blob")]
    public float awarenessRange = 5f;

    [Tooltip("A directly reference to the player's blob size, so we can check how big they are compared to us")]
    public BlobSize playerBlob;

    /// <summary>
    /// The transform that the blob is currently focused on. This will be null if the blob is not focused on anything.
    /// Use this to track another blob this blob might need to know about, such as the player.
    /// </summary>
    public Transform focusedTransform;

    [SerializeField] protected StateBehaviour stateCurrent;
    protected BlobSize mySize;

    public virtual void Start()
    {
        // GetComponents will get all the components of one type as an array
        // This will capture all the states attached to the same gameobject as this state machine
        states = GetComponents<StateBehaviour>();

        // Get a reference to the blob size
        mySize = GetComponent<BlobSize>();

        // Set the machine with the first state found
        ChangeState(states[0].blobState);
    }

    public void Update()
    {
        // Check if we should flee/hunt
        CheckAwareness();

        // Run the current state's Update behaviour
        stateCurrent.StateUpdate();
    }

    /// <summary>
    /// Change the current state to a new state, doing nothing if the state is already active.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(BlobState newState)
    {
        // If the new state matches the current state, do nothing
        if (stateCurrent && stateCurrent.blobState == newState)
            return;
        
        // Loop through our states
        foreach (StateBehaviour state in states)
        {
            // When we find the state which matches the requested state...
            if (state.blobState == newState)
            {
                // Update the current state
                stateCurrent = state;

                // Run the state's Enter behaviour
                state.Enter();

                // Stop looping because we found the state we want
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
            if (playerBlob.IsBiggerThan(mySize))
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
    /// Determine whether or not the blob is currently in danger of being eaten.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsInDanger()
    {
        // Check if the focused transform is in range of the blob
        return IsInRange(focusedTransform, awarenessRange * GetBlobRadius() + 2f);
    }

    /// <summary>
    /// Calculate the direction away from danger.
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 DirectionAwayFromDanger()
    {
        Vector3 direction = transform.position - focusedTransform.position;
        return direction.normalized;
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
