using UnityEngine;
using UnityEditor;

// Mark this Editor as a custom editor for the InvokeAfter class
[CustomEditor(typeof(InvokeAfter))]
public class InvokeAfterDrawer : Editor // Editor class allows us to draw a UnityEngine.Object inspector
{
    // Override OnInspectorGUI to define how the object looks in the inspector
    public override void OnInspectorGUI()
    {
        #region Get the serialized properties
        // To find a serialized property, it must either be public, or marked [SerializeField]
        // Classes not inheriting from UnityEngine.Object must also be marked [System.Serializable]
        // We find properties by name, given as a string (case sensitive like always!)
        SerializedProperty timeMax = serializedObject.FindProperty("timeMax");
        SerializedProperty timeRemaining = serializedObject.FindProperty("timeRemaining");
        SerializedProperty isTicking = serializedObject.FindProperty("isTicking");
        SerializedProperty looping = serializedObject.FindProperty("looping");
        SerializedProperty onTimeout = serializedObject.FindProperty("onTimeout");
        #endregion

        #region  Draw the time remaining/maximum

        ////// TEACHER! Don't get the local Rect at first
        /// Just write 'EditorGUILayout.BeginHorizontal();' at first
        /// Come back and add 'Rect rect = ' later, when doing the progress bar below
        
        // Horizontal group will make the contained GUI stay on one row
        Rect rect = EditorGUILayout.BeginHorizontal();
        // SerializedProperty could hold a large number of types
        // We need to get the underlying value to modify
        EditorGUILayout.LabelField("Timer: ", GUILayout.Width(40)); // GUILayout.Width controls how wide this field is
        timeRemaining.floatValue = EditorGUILayout.FloatField(timeRemaining.floatValue);
        EditorGUILayout.LabelField("of", GUILayout.Width(15));  
        timeMax.floatValue = EditorGUILayout.FloatField(timeMax.floatValue);
        EditorGUILayout.LabelField("secs remaning");
        EditorGUILayout.EndHorizontal();    // We must mark the end of the group
        #endregion
        
        ////// TEACHER! Don't do this at first.
        /// Save this for the end of the lesson.
        /// Write the rest of the script first, then come back as instructed
        #region Draw a progress bar
        // We got a rect from our horizontal group above
        // It is the same size as the horizontal group
        // Therefore, we can use it to draw our progress bar directly below the timer
    
        // Move the rectangle down one row plus one pixel
        rect.y += rect.height + 1;

        // Make the rectangle short in height
        rect.height = 4;

        // Draw the background of the progress bar
        EditorGUI.DrawRect(rect, Color.grey);

        // This COPIES rect, so we can change rectFill without changing rect
        Rect rectFill = rect;

        // Set the width based on a percentage of how much time is remaining
        rectFill.width *= timeRemaining.floatValue / timeMax.floatValue;

        // Draw the filled portion of the progress bar
        EditorGUI.DrawRect(rectFill, Color.white);

        // Use auto-layout to draw a gap of 6 pixels between the timer and the toggles
        // Our manually drawn Rect will appear in this space
        EditorGUILayout.Space(6);
        #endregion

        #region Draw toggles for ticking and looping
        EditorGUILayout.BeginHorizontal();
        isTicking.boolValue = EditorGUILayout.ToggleLeft("Is Ticking", isTicking.boolValue);
        looping.boolValue = EditorGUILayout.ToggleLeft("Looping", looping.boolValue);
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Draw buttons to run functions
        // To run functions on our component, we need to retrieve & cast it into the right class
        InvokeAfter target = serializedObject.targetObject as InvokeAfter;

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(target.isTicking ? "Pause" : "Play"))
        {
            if (target.isTicking)
            {
                target.Pause(); // This is possible because the functions and variables are public
            }
            else
            {
                target.Play();
            }
        }
        if (GUILayout.Button("Stop"))
        {
            target.Stop();
        }
        if (GUILayout.Button("Reset"))
        {
            target.Reset();
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        // Draw the UnityEvent as normal
        EditorGUILayout.PropertyField(onTimeout);

        // If anything changed, apply the changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
