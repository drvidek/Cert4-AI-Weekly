using UnityEngine;

public class NPC : MonoBehaviour
{
    [RandomName]
    public string npcName;

    public StatBlock statBlock;

    [Randomise(0, 1, 0.75f, 1)]
    public Color shirtColor;

    [Randomise(0, 500)]
    public float cash;
}

