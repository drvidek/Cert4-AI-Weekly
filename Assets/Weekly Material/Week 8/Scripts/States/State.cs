using UnityEngine;

public enum BlobState
{
    Idle,
    Walk,
    Hunt,
    Flee
}

public abstract class State : MonoBehaviour
{
    public BlobStateMachine stateMachine;

    public BlobState blobState;

    void Awake()
    {
        stateMachine = GetComponentInParent<BlobStateMachine>();
    }

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

    public abstract void Enter();
    public abstract void UpdateState();

}
