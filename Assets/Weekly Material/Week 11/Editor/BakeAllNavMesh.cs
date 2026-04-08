using UnityEngine;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEditor.SceneManagement;

public class BakeAllNavMesh
{   
    // This attribute adds to our menu (File, Edit, etc) at the path we provide.
    // When we click the menu item, it will run the matching function.
    [MenuItem("Tools/AI/Bake All NavMeshSurface")]
    public static void BakeAll()
    {
        // Find all NavMeshSurface components in the scene
        foreach (NavMeshSurface surface in MonoBehaviour.FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None))
        {
            // Bake each one
            surface.BuildNavMesh();
        }

        // In order to keep our changes, we must mark the active scene as 'dirty' (needing saving)
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
