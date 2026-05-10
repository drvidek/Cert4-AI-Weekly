using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StatBlock))]
public class StatBlockDrawer : PropertyDrawer
{
    bool isExpanded;

    const int lineSeparator = 2;
    const int columnSeparator = 10;
    const int previewWidthMin = 52;
    const int previewDivider = 3;

    // Override to define how tall the property is within the inspector
    // Lists, arrays, classes, anything which folds out needs to override this
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the default height for a field
        float baseHeight = base.GetPropertyHeight(property, label);

        // If we're expanded, make room for 4x the height plus some separators, else just use default height
        return isExpanded ? baseHeight * 4 + lineSeparator * 2 : baseHeight;
    }

    // Override to draw the fields and controls
    // Rect position - the standard rect for the entire property, as if drawn natively
    // SerializedProperty property - the property being inspected (our class), serialized
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
        Rect rectFoldout = new Rect(position);
        // Set the height to the default height of a single field
        rectFoldout.height = base.GetPropertyHeight(property, label);
        // Draw the toggle and label to expand or hide the stats
        isExpanded = EditorGUI.Foldout(rectFoldout, isExpanded, label);

        ////// TEACHER! Do not write this region at first.
        /// Fow now, just use this: 
            // if (!isExpanded)
            // {
            //     return;
            // }
        /// Then skip down from 'Define rects for our stat fields' region
        #region  If not expanded, draw a preview only
        if (!isExpanded)
        {
            // Full width of the property - width of label gives us the width of the field
            float fieldWidth = position.width - EditorGUIUtility.labelWidth - 10;

            // Determine the width of each stat preview, no smaller than minWidth
            // But fill up the 
            // (Divide by 5 for our five stats)
            int previewWidth = (int)Mathf.Max(fieldWidth / 5, previewWidthMin);

            // Get our first rect using
            Rect rectPreview = new Rect(rectFoldout);
            rectPreview.width = previewWidth;
            // Place the rect 
            rectPreview.x = rectFoldout.x + EditorGUIUtility.labelWidth;

            // Draw a red box with the Attack preview
            GUI.color = Color.red;
            GUI.Box(rectPreview, $"Atk: {attack.intValue}");

            // Now move the rect right by one width, and repeat
            rectPreview.x += previewWidth + previewDivider;
            GUI.color = Color.cyan;
            GUI.Box(rectPreview, $"Def: {defence.intValue}");

            rectPreview.x += previewWidth + previewDivider;
            GUI.color = Color.orange;
            GUI.Box(rectPreview, $"SpA: {attackSpecial.intValue}");

            rectPreview.x += previewWidth + previewDivider;
            GUI.color = Color.royalBlue;
            GUI.Box(rectPreview, $"SpD: {defenceSpecial.intValue}");

            rectPreview.x += previewWidth + previewDivider;
            GUI.color = Color.green;
            GUI.Box(rectPreview, $"Spd: {speed.intValue}");

            GUI.color = Color.white;
            return;
        }
        #endregion

        ////// Skip to here at first!
        #region  Define rects for our stat fields
        // Get a new rect based on our foldout rect
        Rect rectAttack = new Rect(rectFoldout);
        // Position it at the bottom of our foldout rect
        rectAttack.y = rectFoldout.max.y;
        // Make the rect slightly less than half the width of the foldout rect
        rectAttack.width *= 0.5f;
        rectAttack.width -= columnSeparator;

        // Create our defence rect based on our attack rect
        Rect rectDefence = new Rect(rectAttack);
        // Change the x position to move it right of the Attack rect
        rectDefence.x = rectAttack.max.x + columnSeparator;

        // Get a rect matching out attack rect
        Rect rectSpecialAttack = new Rect(rectAttack);
        // Place it directly below the attack rect
        rectSpecialAttack.y = rectAttack.max.y + lineSeparator;

        // Do the same with the defence rect
        Rect rectSpecialDefence = new Rect(rectDefence);
        rectSpecialDefence.y = rectSpecialAttack.y;

        // Finally the speed rect goes below our special attack rect
        Rect rectSpeed = new Rect(rectAttack);
        rectSpeed.y = rectSpecialAttack.max.y + lineSeparator;
        #endregion

        #region  Draw the property fields using the rects defined
        EditorGUI.PropertyField(rectAttack, attack);
        EditorGUI.PropertyField(rectDefence, defence);

        EditorGUI.PropertyField(rectSpecialAttack, attackSpecial);
        EditorGUI.PropertyField(rectSpecialDefence, defenceSpecial);

        EditorGUI.PropertyField(rectSpeed, speed);
        #endregion

        // Tell the Editor we've finished editing a property
        EditorGUI.EndProperty();
    }
}
