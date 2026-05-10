using UnityEditor;
using UnityEngine;

namespace Custom
{
    // When inside the Custom namespace, our code assumes it should use Custom.RangeAttribute
    [CustomPropertyDrawer(typeof(RangeAttribute))]
    public class RangeAttributeDrawer : PropertyDrawer
    {
        // This will replace an existing property drawer when the field uses this Attribute
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Begin the property as normal
            EditorGUI.BeginProperty(position, label, property);

            // All PropertyDrawers have an 'attribute' variable which contains the attribute (if it exists)
            // We must cast it into the correct type to access its variables (in this case, .min and .max)
            Custom.RangeAttribute range = attribute as Custom.RangeAttribute;

            // We want our Range to work with both ints and floats
            // Therefore we must change the controls depending on the type of the property
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    // Use an int slider specifically if it is an int
                    property.intValue = EditorGUI.IntSlider(position, label, property.intValue, (int)range.min, (int)range.max);
                    break;
                case SerializedPropertyType.Float:
                    // Use a float slider if it is a float
                    property.floatValue = EditorGUI.Slider(position, label, property.floatValue, range.min, range.max);
                    break;
                default:
                    // If the property is not an int or float, we display an error message.
                    EditorGUI.LabelField(position, "Use Range with int or float.");
                    break;
            }

            EditorGUI.EndProperty();
        }
    }
}