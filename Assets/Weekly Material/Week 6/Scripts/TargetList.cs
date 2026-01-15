using System.Collections.Generic;
using UnityEngine;

public class TargetList : MonoBehaviour
{
    // Keep track of all the targets in range to shoot at
    public List<Transform> currentTargets = new List<Transform>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the thing that entered is a valid target...
        if (collision.gameObject.CompareTag("Target"))
        {
            // Add it to the list of targets
            currentTargets.Add(collision.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // We can safely remove from a list even if the item isn't actually on it
        currentTargets.Remove(collision.transform);
    }
}
