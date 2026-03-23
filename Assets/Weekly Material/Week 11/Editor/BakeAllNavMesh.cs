using UnityEngine;
using Unity.AI.Navigation;
using UnityEditor;

public class BakeAllNavMesh
{
    [MenuItem("Bake All NavMesh Surfaces", menuItem = "Tools/AI/Bake All NavMeshSurface")]
    public static void BakeAll()
    {
        foreach (NavMeshSurface surface in MonoBehaviour.FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None))
        {
            surface.BuildNavMesh();
        }
    }
}
