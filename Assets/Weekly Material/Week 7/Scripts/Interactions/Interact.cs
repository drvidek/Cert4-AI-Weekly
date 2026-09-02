using UnityEngine;

public class Interact : MonoBehaviour
{
    [Tooltip("Set a game object other than this as the object triggering the interaction.")]
    [SerializeField] private GameObject interactor;
    private Interaction currentInteraction;

    void Start()
    {
        if (!interactor)
        {
            interactor = gameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteraction)
            {
                currentInteraction.Interact(interactor);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // If we have a current interaction, don't look for a new one
        if (currentInteraction)
            return;

        // If other has no Interaction, the result will be null
        currentInteraction = other.GetComponent<Interaction>();
    }

    void OnTriggerExit(Collider other)
    {
        // If the current interaction matches an interaction on other
        if (currentInteraction == other.GetComponent<Interaction>())
        {
            // Remove our current interaction
            currentInteraction = null;
        }
    }
}
