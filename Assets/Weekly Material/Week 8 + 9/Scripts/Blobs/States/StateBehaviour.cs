using UnityEngine;

// 'abstract' means we can't use this class directly,
// we have to inherit and extend the class to use it
public abstract class StateBehaviour : MonoBehaviour
{
    protected StateMachine stateMachine;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    /// <summary>
    /// Change to a different state, triggering the new state's Enter behaviour.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(string newState)
    {
        stateMachine.ChangeState(newState);
    }
    
    /// <summary>
    /// Behaviour to run when newly entering this state.
    /// </summary>
    public abstract void Enter();
    
    /// <summary>
    /// Behaviour to run every frame while this state is active.
    /// </summary>
    public abstract void StateUpdate();

    /// <summary>
    /// Implement and return the key associated with this state.
    /// </summary>
    public abstract string StateKey();
}