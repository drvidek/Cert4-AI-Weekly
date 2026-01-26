using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Lock and hide the cursor at the start of the game
        SetCursorLocked(true);
    }


    /// <summary>
    /// Sets the cursor state to locked or unlocked.
    /// </summary>
    /// <param name="locked"></param>
    public void SetCursorLocked(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}
