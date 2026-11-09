using UnityEngine;

// Namespaces allow us to divide our code into groups
// This is helpful when scripts have matching names, e.g. UI 'button' vs. in-game 'button'
// Using a Namespace means we can create our own 'Range' attribute without conflicting with the built in one
namespace Custom
{
    // PropertyAttribute is the base class for all custom Attributes
    // 'NameHereAttribue' will be called [NameHere] in script
    public class RangeAttribute : PropertyAttribute
    {
        // Define two floats which our attribute holds
        public float min, max;

        // When we use the Range attribute, we must construct it with two floats
        // i.e. [Range(0, 10)]
        public RangeAttribute(float min, float max)
        {
            // Assign the two floats to the min/max values of this attribute
            this.min = min;
            this.max = max;
        }
    }
}