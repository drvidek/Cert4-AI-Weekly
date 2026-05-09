using UnityEngine;
using UnityEngine.Events;

public class InvokeAfter : MonoBehaviour
{
    public bool isTicking;
    public float timeMax;
    public float timeRemaining;
    public bool looping;

    public UnityEvent onTimeout;

    // Update is called once per frame
    void Update()
    {
        // If we're not ticking, do nothing
        if (!isTicking)
        {
            return;
        }

        // Count down by seconds passed
        timeRemaining -= Time.deltaTime;

        // If there's no time remaining...
        if (timeRemaining <= 0)
        {
            // Invoke the UnityEvent
            onTimeout.Invoke();

            // If we should loop...
            if (looping)
            {
                // Add our maximum time (this respects negative values)
                timeRemaining += timeMax;
            }
            else
            {
                // Else, stop the countdown
                Stop();
            }
        }
    }

    public void Play()
    {
        // If there's no time left...
        if (timeRemaining <= 0)
        {
            // Show a warning that the alarm will immediately trigger the timeout
            Debug.LogWarning("Trying to play a timer which is at 0 sec remaining (immediate timeout).\nUse ResetAndPlay if this is not desired behaviour.");
        }

        // Start ticking
        isTicking = true;
    }

    public void Pause()
    {
        // Stop ticking
        isTicking = false;
    }

    public void Stop()
    {
        // Stop ticking and reduce time to 0
        isTicking = false;
        timeRemaining = 0;
    }

    public void Reset()
    {
        // Set time to maximum
        timeRemaining = timeMax;
    }

    public void ResetAndPlay()
    {
        Reset();
        Play();
    }
}
