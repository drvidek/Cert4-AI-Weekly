using UnityEngine;

namespace Week15
{
    public class NPC : MonoBehaviour
    {
        public StatBlock statBlock;

        /// Initially use the built-in Range attribute, then change to Custom
        [Custom.Range(-10, 10)]
        public float playerRelationshipLevel;

        // Implement after the RandomName attribute is done
        [RandomName]
        public string npcName;

        ////// TEACHER! Thiss is where the new content starts

        // Implement each of these after Randomise attribute is done
        [Randomise(0, 500)]
        public float cash;

        [Randomise(0, 1, 0.75f, 1)]
        public Color shirtColor;

        // Implemented to demonstrate the property type error
        [Randomise]
        public string greeting;
    }
}

