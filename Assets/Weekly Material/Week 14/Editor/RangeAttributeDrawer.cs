using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Custom.RangeAttribute))]
public class RangeAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Custom.RangeAttribute range = attribute as Custom.RangeAttribute;

        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = EditorGUI.IntSlider(position, label, property.intValue, (int)range.min, (int)range.max);
                break;
            case SerializedPropertyType.Float:
                property.floatValue = EditorGUI.Slider(position, label, property.floatValue, range.min, range.max);
                break;
            default:
                EditorGUI.LabelField(position, "Use Range with int or float.");
                break;
        }

        EditorGUI.EndProperty();
    }
}
