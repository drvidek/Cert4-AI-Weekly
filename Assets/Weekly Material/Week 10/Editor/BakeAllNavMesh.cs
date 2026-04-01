using UnityEngine;
using Unity.AI.Navigation;
using UnityEditor;

public class BakeAllNavMesh
{
    [MenuItem("Tools/AI/Bake All NavMeshSurface")]
    public static void BakeAll()
    {
        foreach (NavMeshSurface surface in MonoBehaviour.FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None))
        {
            surface.BuildNavMesh();
        }
    }
}
