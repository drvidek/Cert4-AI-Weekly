using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StatBlock))]
public class StatBlockDrawer : PropertyDrawer
{
    bool isExpanded;

    // These constants will help us keep spacing and sizing consistent
    const int lineSeparator = 2;
    const int columnSeparator = 10;
    const int previewWidthMin = 52;
    const int previewDivider = 3;

    // Draw a preview box for a stat with a given color, and return the rect for the next preview
    private Rect DrawPreviewBox(Rect rect, string label, int value, Color color, int width)
    {
        GUI.color = color;
        GUI.Box(rect, $"{label}: {value}");
        rect.x += width + previewDivider;
        return rect;
    }

    // Override GetPropertyHeight() to define how tall the property is within the inspector
    // Lists, arrays, classes, and anything which folds out MUST override this
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the default height for a single field
        float baseHeight = base.GetPropertyHeight(property, label);

        // When expanded, we need room for the label, and three rows of stats
        // So we need four times the default height if we're expanded (plus some separation)
        return isExpanded ? (baseHeight * 4) + (lineSeparator * 2) : baseHeight;
    }

    // Override OnGUI() to draw our fields and controls
    // Rect position - equal to one standard-sized field in the inspector, placed at the correct vertical position for this property
    // SerializedProperty property - the property being inspected (our custom class), serialized
    // GUIContent label - the default label for the property
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Tell the Editor we're about to edit a serialized property
        // (Note that, confusingly, the parameter order is different to OnGUI)
        EditorGUI.BeginProperty(position, label, property);

        // Since we're starting with a Property, not an Object, we need to find Property Relatives
        SerializedProperty attack = property.FindPropertyRelative("attack");
        SerializedProperty defence = property.FindPropertyRelative("defence");
        SerializedProperty attackSpecial = property.FindPropertyRelative("specialAttack");
        SerializedProperty defenceSpecial = property.FindPropertyRelative("specialDefence");
        SerializedProperty speed = property.FindPropertyRelative("speed");

        // Define a rect which is the label and foldout for the inspector
        // Use the default rect for this property 
        Rect rectFoldout = new Rect(position)
        {
            // Set the height to the default height of a single field
            height = base.GetPropertyHeight(property, label)
        };

        // Draw the toggle and label to expand or hide the stats
        // Only display the label if the full control is expanded
        isExpanded = EditorGUI.Foldout(rectFoldout, isExpanded, !isExpanded ? GUIContent.none : label);

        ////// TEACHER! Do not write this region at first.
        /// Fow now, just use this: 

        // if (!isExpanded)
        // {
        //     return;
        // }

        /// Then skip down from 'Define rects for our stat fields' region
        /// Return here after done with the next region
        #region  If not expanded, draw a preview only
        if (!isExpanded)
        {
            // Determine the width for each stat preview, no smaller than min width
            // (Divide by 5 for our five stats)
            int previewWidth = (int)Mathf.Max(position.width / 5, previewWidthMin);

            // Get our first rect based on the foldout
            Rect rectPreview = new Rect(rectFoldout)
            {
                width = previewWidth,
                // Place the rect to the right of the label
                x = rectFoldout.x
            };

            // Draw each preview box and return the next preview box's rect
            rectPreview = DrawPreviewBox(rectPreview, "Atk", attack.intValue, Color.red, previewWidth);
            rectPreview = DrawPreviewBox(rectPreview, "Def", defence.intValue, Color.cyan, previewWidth);
            rectPreview = DrawPreviewBox(rectPreview, "SpA", attackSpecial.intValue, Color.orange, previewWidth);
            rectPreview = DrawPreviewBox(rectPreview, "SpD", defenceSpecial.intValue, Color.royalBlue, previewWidth);
            rectPreview = DrawPreviewBox(rectPreview, "Spd", speed.intValue, Color.green, previewWidth);

            GUI.color = Color.white;

            // Return early if not expanded, only showing the preview boxes
            return;
        }
        #endregion

        ////// TEACHER! Skip to here at first!
        #region  Define rects for our stat fields
        // Get a new rect based on our foldout rect
        Rect rectAttack = new Rect(rectFoldout)
        {
            // Position it below our foldout rect
            y = rectFoldout.max.y,
            // Make the rect slightly less than half the width of our whole property width
            width = position.width / 2f - columnSeparator
        };

        // Create our defence rect based on our attack rect
        Rect rectDefence = new Rect(rectAttack)
        {
            // Change the x position to move it right of the Attack rect
            x = rectAttack.max.x + columnSeparator
        };

        // Get a rect matching our attack rect
        Rect rectSpecialAttack = new Rect(rectAttack)
        {
            // Place it below the attack rect with a small space
            y = rectAttack.max.y + lineSeparator
        };

        // Do the same with the defence rect
        Rect rectSpecialDefence = new Rect(rectDefence)
        {
            // This time we can use the y position of the special attack rect
            y = rectSpecialAttack.y
        };

        // Finally the speed rect goes below our special attack rect
        Rect rectSpeed = new Rect(rectAttack)
        {
            // Place it below the special attack rect with a small space
            y = rectSpecialAttack.max.y + lineSeparator
        };
        #endregion

        #region  Draw the property fields using the rects defined
        // Use the rectAttack to draw the attack stat, and so on
        EditorGUI.PropertyField(rectAttack, attack);
        EditorGUI.PropertyField(rectDefence, defence);
        EditorGUI.PropertyField(rectSpecialAttack, attackSpecial);
        EditorGUI.PropertyField(rectSpecialDefence, defenceSpecial);
        EditorGUI.PropertyField(rectSpeed, speed);
        #endregion

        // Tell the Editor we've finished editing a property
        // This is similar to serializedObject.ApplyModifiedProperties(), but for individual properties
        EditorGUI.EndProperty();
    }

}
