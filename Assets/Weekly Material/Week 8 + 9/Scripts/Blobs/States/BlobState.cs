using UnityEngine;

public abstract class BlobState : StateBehaviour
{
    /// <summary>
    /// Move the state machine's transform by the given movement vector. This does not account for speed or time.
    /// </summary>
    /// <param name="movement"></param>
    public void Move(Vector3 movement)
    {
        stateMachine.transform.position += movement;
    }
}
