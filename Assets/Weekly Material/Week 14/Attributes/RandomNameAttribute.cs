using UnityEngine;

public class RandomNameAttribute : PropertyAttribute
{
    // A fallback list of options to use
    public static readonly string[] defaultOptions = new string[] { "Adam", "Brenda", "Clarence", "Din", "Edna" };

    // Load a text asset and split it based on line-breaks
    public static readonly string[] loadedOptions = Resources.Load<TextAsset>("NPC Names").text.Split('\n');

    public string GetRandom()
    {
        // If we don't have loaded options, use default options as a fallback
        if (loadedOptions == null || loadedOptions.Length == 0)
        {
            return GetRandom(defaultOptions);
        }
        
        // Otherwise get from the loaded options
        return GetRandom(loadedOptions);
    }
    
    private string GetRandom(string[] array)
    {
        int index = Random.Range(0, array.Length);
        // Return a random value from the array
        return array[index];
    }
}
