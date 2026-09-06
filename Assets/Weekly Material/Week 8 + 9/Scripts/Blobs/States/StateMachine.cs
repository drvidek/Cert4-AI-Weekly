using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private StateBehaviour[] states;
    [SerializeField] protected StateBehaviour stateCurrent;

    public virtual void Start()
    {
        // Set the machine with the first state found
        ChangeState(states[0].StateKey());
    }

    public virtual void Update()
    {
        // Run the current state's Update behaviour
        stateCurrent.StateUpdate();
    }

    /// <summary>
    /// Change the current state to a new state, doing nothing if the state is already active.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(string newState)
    {
        // If the new state matches the current state, do nothing
        if (stateCurrent && stateCurrent.StateKey() == newState)
            return;
        
        // Loop through our states
        foreach (StateBehaviour state in states)
        {
            // When we find the state which matches the requested state...
            if (state.StateKey() == newState)
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

}
