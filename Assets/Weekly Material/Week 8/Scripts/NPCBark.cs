using UnityEngine;

public class NPCBark : MonoBehaviour
{
    // The bark message to display when the player enters the trigger
    [SerializeField] private string bark;

    // Reference to the TemporaryMessage component that handles displaying messages
    [SerializeField] private TemporaryMessage barkPanel;

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        // If an object with the "Player" tag enters the trigger...
        if (other.CompareTag("Player"))
        {
            // Display the bark message for 5 seconds
            barkPanel.DisplayMessageFor(bark, 5f);
        }
    }
}