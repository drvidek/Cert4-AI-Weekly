using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RandomNameAttribute))]
public class RandomNameAttributeDrawer : PropertyDrawer
{
    const int buttonWidth = 20;
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.LabelField(position, "Use RandomName with a string field");
            return;
        }

        // Prepare Unity Editor to modify a serialized property
        EditorGUI.BeginProperty(position, label, property);

        //Get the attribute given to the property
        RandomNameAttribute randomName = attribute as RandomNameAttribute;

        // Get a Rect matching the default one
        Rect rectProperty = new Rect(position);

        // Cut off the width of the button
        rectProperty.width -= buttonWidth;

        // Get another rect
        Rect rectButton = new Rect(rectProperty);

        // Set the width directly
        rectButton.width = buttonWidth;

        // Position it to the right of the other rect
        rectButton.x = rectProperty.max.x;
        
        // Draw whatever field is appropriate for the property
        EditorGUI.PropertyField(rectProperty, property);

        // Draw a small button labeled 'R', and if clicked...
        if (GUI.Button(rectButton, "R"))
        {
            // Set the underlying string value of the property to a random name
            property.stringValue = randomName.Get();
        }

        EditorGUI.EndProperty();
    }
}