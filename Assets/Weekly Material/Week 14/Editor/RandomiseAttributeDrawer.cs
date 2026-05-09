using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(RandomiseAttribute))]
public class RandomiseAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        RandomiseAttribute random = attribute as RandomiseAttribute;

        #region Define rectProperty and rectButton Rects
        int buttonWidth = 20;
        // Get a Rect matching the default one
        Rect rectProperty = new Rect(position);
        // Cut off the width of the button
        rectProperty.width -= buttonWidth;

        // Get another rect for the button
        Rect rectButton = new Rect(rectProperty);
        // Set the width directly
        rectButton.width = buttonWidth;
        // Position it to the right of the other rect
        rectButton.x = rectProperty.max.x;
        #endregion

        // Draw whatever field is appropriate for the property with the new rect
        EditorGUI.PropertyField(rectProperty, property);

        // If the user presses the R (random) button...
        if (GUI.Button(rectButton, new GUIContent("R", "Randomise the value")))
        {
            float w = 0, x = 0, y = 0, z = 0, h = 1, s = 1, v = 1;
            int ix = 0, iy = 0, iz = 0;

            #region Calculate random values as needed
            switch (random.rangeCount)
            {
                // Fully randomise with no min/max provided
                case 0:
                    x = random.Float();
                    y = random.Float();
                    z = random.Float();
                    w = random.Float();

                    h = random.Float01();
                    s = random.Float01();
                    v = random.Float01();

                    ix = random.Int();
                    iy = random.Int();
                    iz = random.Int();
                    break;
                // Only randomise first value within range
                case 1:
                    x = random.Float(0);

                    h = random.Float01(0);

                    ix = random.Int(0);
                    break;
                // Randomise first two values within ranges
                case 2:
                    x = random.Float(0);
                    y = random.Float(1);

                    h = random.Float01(0);
                    s = random.Float01(1);

                    ix = random.Int(0);
                    iy = random.Int(1);
                    break;
                case 3:
                    x = random.Float(0);
                    y = random.Float(1);
                    z = random.Float(2);

                    h = random.Float01(0);
                    s = random.Float01(1);
                    v = random.Float01(2);

                    ix = random.Int(0);
                    iy = random.Int(1);
                    iz = random.Int(2);
                    break;
                case 4:
                    x = random.Float(0);
                    y = random.Float(1);
                    z = random.Float(2);
                    w = random.Float(3);

                    ix = random.Int(0);
                    iy = random.Int(1);
                    iz = random.Int(2);

                    h = random.Float01(0);
                    s = random.Float01(1);
                    v = random.Float01(2);
                    break;
            }
            #endregion

            // Depending on what type of property it is, apply our random values
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.intValue = ix;
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = x;
                    break;
                case SerializedPropertyType.Color:
                    property.colorValue = Color.HSVToRGB(h, s, v);
                    break;
                case SerializedPropertyType.Vector2:
                    property.vector2Value = new Vector2(x, y);
                    break;
                case SerializedPropertyType.Vector3:
                    property.vector3Value = new Vector3(x, y, z);
                    break;
                case SerializedPropertyType.Quaternion:
                    property.quaternionValue = new Quaternion(x, y, z, w);
                    break;
                case SerializedPropertyType.Vector2Int:
                    property.vector2IntValue = new Vector2Int(ix, iy);
                    break;
                case SerializedPropertyType.Vector3Int:
                    property.vector3IntValue = new Vector3Int(ix, iy, iz);
                    break;
                default:
                    Debug.LogWarning("RandomiseAttribute doesn't work with " + property.type);
                    break;
            }
        }

        EditorGUI.EndProperty();
    }
}