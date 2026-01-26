using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogueLines;

    private MessageQueue messageQueue;

    void Start()
    {
        // Find and store a reference to the MessageQueue in the scene
        messageQueue = FindFirstObjectByType<MessageQueue>();
    }

    public void ShowDialogue()
    {
        // Use the MessageQueue to display the dialogue lines
        messageQueue.OpenAndQueue(dialogueLines);
    }
}
