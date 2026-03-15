using UnityEngine;

public enum BlobState
{
    Idle,
    Walk,
    Hunt,
    Flee
}

// 'abstract' means we can't use this class directly,
// we have to inherit and extend the class to use it
public abstract class StateBehaviour : MonoBehaviour
{
    protected BlobStateMachine stateMachine;

    [Tooltip("Which state is associated with this behaviour?")]
    public BlobState blobState;

    // Awake is like Start, but runs before anything else
    void Awake()
    {
        stateMachine = GetComponent<BlobStateMachine>();

    }

    /// <summary>
    /// Change to a different state, triggering the new state's Enter behaviour.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(BlobState newState)
    {
        stateMachine.ChangeState(newState);
    }
    
    /// <summary>
    /// Move the state machine's transform by the given movement vector. This does not account for speed or time.
    /// </summary>
    /// <param name="movement"></param>
    public void Move(Vector3 movement)
    {
        stateMachine.transform.position += movement;
    }

    /// <summary>
    /// Behaviour to run when newly entering this state.
    /// </summary>
    public abstract void Enter();
    
    /// <summary>
    /// Behaviour to run every frame while this state is active.
    /// </summary>
    public abstract void StateUpdate();
}