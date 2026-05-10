using UnityEngine;

public class NPC : MonoBehaviour
{
    ////// This is created for week 13
    public StatBlock statBlock;

    ////// All below is created for Week 14+
    
    /// Initially use the built-in Range attribute, then change to Custom
    [Custom.Range(-10, 10)]
    public float playerRelationshipLevel;

    // Implement after the RandomName attribute is done
    [RandomName]
    public string npcName;

    // Implement each of these after Randomise attribute is done
    [Randomise(0, 500)]
    public float cash;
    
    [Randomise(0, 1, 0.75f, 1)]
    public Color shirtColor;

    // Implemented to demonstrate the property type error
    [Randomise]
    public string greeting;
}

