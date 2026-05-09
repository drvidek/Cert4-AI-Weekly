using UnityEngine;

public class RandomNameAttribute : PropertyAttribute
{
    // A temporary list of options to use
    public static readonly string[] defaultOptions = new string[] { "Adam", "Brenda", "Clarence", "Din", "Edna" };

    public static readonly string[] options = Resources.Load<TextAsset>("NPC Names").text.Split('\n');

    public string GetDefault()
    {
        int index = Random.Range(0, defaultOptions.Length);
        return defaultOptions[index];
    }

    public string Get()
    {
        if (options == null || options.Length == 0)
            return GetDefault();
            
        int index = Random.Range(0, options.Length);
        return options[index];
    }
}
