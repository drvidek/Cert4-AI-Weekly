using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StatBlock))]
public class StatDrawer : PropertyDrawer
{
    bool isExpanded;

    const int lineSeparator = 2;
    const int columnSeparator = 10;
    const int previewWidthMin = 52;
    const int previewDivider = 3;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float baseHeight = base.GetPropertyHeight(property, label);
        return isExpanded ? baseHeight * 4 + lineSeparator * 2 : baseHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty attack = property.FindPropertyRelative("attack");
        SerializedProperty defence = property.FindPropertyRelative("defence");
        SerializedProperty attackSpecial = property.FindPropertyRelative("specialAttack");
        SerializedProperty defenceSpecial = property.FindPropertyRelative("specialDefence");
        SerializedProperty speed = property.FindPropertyRelative("speed");

        Rect rectFoldout = new Rect(position);

        rectFoldout.height = base.GetPropertyHeight(property, label);

        isExpanded = EditorGUI.Foldout(rectFoldout, isExpanded, label);

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

        #region  Define rects for our stat fields
        Rect rectAttack = new Rect(rectFoldout);
        rectAttack.y = rectFoldout.max.y;
        rectAttack.height = rectFoldout.height;
        // Make the rect slightly less than half the width of the inspector
        rectAttack.width *= 0.5f;
        rectAttack.width -= columnSeparator;

        // Build our Defence rect based on our Attack rect
        Rect rectDefence = new Rect(rectAttack);
        // Just change the x position to move it right
        rectDefence.x = rectAttack.max.x + columnSeparator;

        // Now we have templates for our following rects!
        Rect rectAttackSpecial = new Rect(rectAttack);
        rectAttackSpecial.y = rectAttack.max.y + lineSeparator;

        Rect rectDefenceSpecial = new Rect(rectDefence);
        rectDefenceSpecial.y = rectAttackSpecial.y;

        Rect rectSpeed = new Rect(rectAttack);
        rectSpeed.y = rectAttackSpecial.max.y + lineSeparator;
        #endregion

        #region  Draw the property fields using the rects defined
        EditorGUI.PropertyField(rectAttack, attack);
        EditorGUI.PropertyField(rectDefence, defence);

        EditorGUI.PropertyField(rectAttackSpecial, attackSpecial);
        EditorGUI.PropertyField(rectDefenceSpecial, defenceSpecial);

        EditorGUI.PropertyField(rectSpeed, speed);
        #endregion

        EditorGUI.EndProperty();
    }
}
