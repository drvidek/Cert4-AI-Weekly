using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class PrefabPlacer : EditorWindow
{

    [MenuItem("Tools/Scene Helper/Prefab Placer")]
    public static void Open()
    {
        GetWindow<PrefabPlacer>();
    }

    public enum PlacementType
    {
        SceneObject,
        PrefabDisconnected,
        PrefabConnected,
    }

    PlacementType placementType;

    GameObject prefab = null;

    string objectName = "";

    Vector3 scenePosition = Vector3.zero;

    void OnGUI()
    {
        // This allows us to check if any GUI fields have been changed this frame
        EditorGUI.BeginChangeCheck();

        // EnumPopup can work with any enum, so it returns a generic Enum
        // We must convert (cast) it into the type of enum we want 
        placementType = (PlacementType)EditorGUILayout.EnumPopup("Placement Type", placementType);

        // If the field was changed...
        if (EditorGUI.EndChangeCheck())
        {
            // We want to make sure the stored object is the correct asset type
            // It would be better to validate if it is a prefab or not, but this is simpler.
            prefab = null;
        }

        bool allowSceneObjects = placementType == PlacementType.SceneObject;

        // A field for the prefab asset
        // ObjectField works with any UnityEngine.Object
        // We need to provide the type of Object for this field with 'typeof()'
        // We must also cast the result from an Object into the GameObject type
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab to create: ", prefab, typeof(GameObject), allowSceneObjects);

        // The following GUI shouldn't appear if there's nothing in the prefab field
        if (!prefab)
        {
            // Show an instruction, and do nothing else
            EditorGUILayout.LabelField("Please place a GameObject prefab in the above field.");
            return;
        }

        // A field for the name
        objectName = EditorGUILayout.TextField("Object name: ", objectName);

        // A field for the position
        scenePosition = EditorGUILayout.Vector3Field("Position: ", scenePosition);

        bool isValid = objectName != "";

        // Disable the button if the provided name is blank
        EditorGUI.BeginDisabledGroup(!isValid);

        // Click this button to spawn the prefab in the scene
        if (GUILayout.Button("Spawn prefab"))
        {
            Spawn();
        }

        EditorGUI.EndDisabledGroup();

        // Show an error message if no name is entered
        if (!isValid)
        {
            EditorGUILayout.LabelField("Please enter a name for the object.");
        }



        // This final line is only needed if doing the Handles extension below.
        SceneView.RepaintAll();
    }

    void Spawn()
    {
        GameObject spawnedObject = null;

        switch (placementType)
        {
            // If the prefab should be connected, 
            case PlacementType.PrefabConnected:
                spawnedObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                break;
            // We can 'stack' cases which should have matching behaviour
            case PlacementType.PrefabDisconnected:
            case PlacementType.SceneObject:
                spawnedObject = Instantiate(prefab);
                break;
        }

        // This will allow us to undo the prefabs we add like any build in Unity edit function!
        Undo.RegisterCreatedObjectUndo(spawnedObject, "Spawn object");

        // Set up the spawned object as desired
        spawnedObject.name = objectName;
        spawnedObject.transform.position = scenePosition;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
