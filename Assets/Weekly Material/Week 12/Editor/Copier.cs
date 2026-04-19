using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

// EditorWindow is the base class for all Unity Editor windows, including the official ones.
public class Copier : EditorWindow
{
    // This MenuItem function will allow us to open a copy of our editor window.
    [MenuItem("Tools/Scene Helper/Copier")]
    public static void Open()
    {
        // If the Copier window is already open, focus it. Otherwise, open a new instance of it.
        GetWindow<Copier>();
    }

    // How many copies we want to make
    int copyCount;

    // Implemet 'OnGUI' to define what should appear in the editor window
    // OnGUI() can also be used by any MonoBehaviour to draw on screen
    void OnGUI()
    {
        // EditorGUILayout class gives us editor-specific fields which auto-layout in size
        // Alternatively, EditorGUI class allows you to specify where/how to draw things

        // Get the value for 'copyCount' from an integer field,
        // using the label 'Copies to make:' and display the value of 'copyCount'
        copyCount = EditorGUILayout.IntField("Copies to make: ", copyCount);

        // Enforce a value of 0 or larger, to prevent errors
        copyCount = Mathf.Max(0, copyCount);

        // 'Selection' class helps us read/manage what is selected in the editor
        // Using this tool is only valid if we have at least one game object selected
        bool isValid = Selection.gameObjects.Length > 0;

        // Disable (grey out) any following GUI if the condition is met.
        // This also helps prevent errors
        EditorGUI.BeginDisabledGroup(!isValid);

        // GUILayout is the same idea as EditorGUILayout,
        // but it has widgets which can be drawn in editor and during gameplay
        // such as buttons:
        if (GUILayout.Button("Create Copies"))
        {
            Copy();
        }

        // Stop making GUI non-interactible based on the earlier condition
        EditorGUI.EndDisabledGroup();

        // If there's a user error, display a message saying why
        if (!isValid)
        {
            // GUI inside a failed 'if' statement won't display at all!
            EditorGUILayout.LabelField("Please select game object/s in the active scene.");
        }

    }

    void Copy()
    {
        // For each Game Object selected currently...
        foreach (var obj in Selection.gameObjects)
        {
            // If the object is in a scene (i.e. not an asset)
            if (obj.scene.IsValid())
            {
                // Instantiate the object for the number of copies to make
                for (int i = 0; i < copyCount; i++)
                {
                    Instantiate(obj);
                }
            }
        }

        // Mark the scene as dirty
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
