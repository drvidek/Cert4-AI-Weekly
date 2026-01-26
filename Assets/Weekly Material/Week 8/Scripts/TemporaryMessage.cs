using UnityEngine;
using TMPro;

public class TemporaryMessage : MonoBehaviour
{
    // The component which will display the text message
    [SerializeField] private TextMeshProUGUI textDisplay;

    // The current remaining duration for which the message should be displayed
    private float durationCurrent;

    void Start()
    {
        // Initially disable the panel
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Reduce the remaining duration
        durationCurrent -= Time.deltaTime;

        // If the duration has elapsed, disable the panel
        if (durationCurrent <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// Displays a message for a specified duration.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="duration"></param>
    public void DisplayMessageFor(string message, float duration)
    {
        // If a message is already being displayed, do nothing
        if (durationCurrent > 0)
        {
            return;
        }

        // Enable the panel and set the text and duration
        gameObject.SetActive(true);
        textDisplay.text = message;
        durationCurrent = duration;
    }
}
