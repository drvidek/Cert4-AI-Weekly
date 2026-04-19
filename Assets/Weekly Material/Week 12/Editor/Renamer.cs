using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Renamer : EditorWindow
{
    [MenuItem("Tools/Scene Helper/Renamer")]
    public static void Open()
    {
        GetWindow<Renamer>();
    }

    string objectName = "";
    
    void OnGUI()
    {
        // TextField and TextArea allow two different ways to get strings (small or big)
        objectName = EditorGUILayout.TextField("Name basis: ", objectName);

        // Here we have two different validations:
        // 1. Something in the name field
        bool nameValid = objectName != "";
        // 2. Something selected in the scene
        bool selectionValid = Selection.gameObjects.Length > 0;

        // Both are required to be valid
        bool isValid = nameValid && selectionValid;

        EditorGUI.BeginDisabledGroup(!isValid);

        if (GUILayout.Button("Rename"))
        {
            Rename();
        }

        EditorGUI.EndDisabledGroup();

        // Show the correct error message/s based on the valid checks
        if (!nameValid)
        {
            EditorGUILayout.LabelField("Please enter a name.");
        }
        if (!selectionValid)
        {
            EditorGUILayout.LabelField("Please select at least one game object in the scene");
        }
    }

    void Rename()
    {
        int length = Selection.gameObjects.Length;

        for (int i = 0; i < length; i++)
        {
            // Rename each selected game object
            Selection.gameObjects[i].name = objectName + i.ToString();
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
