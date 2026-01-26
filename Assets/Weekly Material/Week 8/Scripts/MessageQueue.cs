using UnityEngine;
using TMPro;

public class MessageQueue : MonoBehaviour
{
    // The TextMeshProUGUI component to display messages
    [SerializeField] private TextMeshProUGUI messageText;
    // The array of messages to display
    private string[] messages = new string[0];
    // The index of the current message being displayed
    private int currentMessageIndex = 0;

    // A reference to the player to freeze movement while messages are displayed
    private Player3D player;

    private CursorManager cursorManager;

    void Start()
    {
        // Find and store a reference to the Player3D component in the scene
        player = FindFirstObjectByType<Player3D>();

        // Find the CursorManager in the scene
        cursorManager = FindFirstObjectByType<CursorManager>();

        // Initially disable the message queue display
        gameObject.SetActive(false);
    }


    /// <summary>
    /// Opens the message queue with a set of messages.
    /// </summary>
    /// <param name="newMessages"></param>
    public void OpenAndQueue(string[] newMessages)
    {
        // Enable the display
        gameObject.SetActive(true);

        // Set the messages and reset the index
        messages = newMessages;
        currentMessageIndex = 0;

        // If there are messages, display the first one
        if (messages.Length > 0)
        {
            // Freeze the player
            player.SetFreeze(true);

            // Lock and hide the cursor
            cursorManager.SetCursorLocked(false);

            // Display the current message
            DisplayCurrentMessage();
        }
        // If there are no messages, close the display
        else
        {
            ClearAndClose();
        }
    }

    /// <summary>
    /// Shows the next message in the queue, or closes it if there are no more messages.
    /// </summary>
    public void ShowNextMessage()
    {
        // If there are no messages, do nothing
        if (messages.Length == 0)
        {
            return;
        }

        // Move to the next message
        currentMessageIndex++;

        // If we've reached the end of the messages, clear and close
        if (currentMessageIndex >= messages.Length)
        {
            ClearAndClose();
        }
        // Otherwise, display the current message
        else
        {
            DisplayCurrentMessage();
        }
    }

    private void DisplayCurrentMessage()
    {
        // Update the text display with the current message
        messageText.text = messages[currentMessageIndex];
    }

    private void ClearMessages()
    {
        // Clear the messages and reset the index
        messageText.text = "";
        messages = new string[0];
        currentMessageIndex = 0;
    }

    private void Close()
    {
        // Disable the display
        gameObject.SetActive(false);
        // Unfreeze the player
        player.SetFreeze(false);
        // Lock and hide the cursor
        cursorManager.SetCursorLocked(true);
    }

    /// <summary>
    /// Clears all messages and closes the message queue display.
    /// </summary>
    public void ClearAndClose()
    {
        ClearMessages();
        Close();
    }
}
