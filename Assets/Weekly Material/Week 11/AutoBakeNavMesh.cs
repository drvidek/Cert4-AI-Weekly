using UnityEngine;
using Unity.AI.Navigation;
using UnityEditor;

// This Attribute means the script will execute Start and Update in edit mode
[ExecuteInEditMode]
public class AutoBakeNavMesh : MonoBehaviour
{
    public float secondsBetweenBake = 60f;

    private double startupTime;

    void OnEnable()
    {
        startupTime = EditorApplication.timeSinceStartup;
        print("AutoBake started.");
    }

    void Update()
    {
        // If we're in live gameplay, do nothing
        if (Application.isPlaying)
            return;

        if (EditorApplication.timeSinceStartup > startupTime + secondsBetweenBake)
        {
            Rebake();
            startupTime = EditorApplication.timeSinceStartup;
        }
    }

    void Rebake()
    {
        print("AutoBake activating.");
        foreach (NavMeshSurface surface in FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None))
        {
            surface.BuildNavMesh();
        }
    }
}
