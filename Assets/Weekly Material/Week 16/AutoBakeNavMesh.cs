using UnityEngine;
using Unity.AI.Navigation;

// This Attribute means the script will execute Awake and Update in edit mode
[ExecuteInEditMode]
public class AutoBakeNavMesh : MonoBehaviour
{
    // A reference to the NavMesh Surface you want to automatically bake
    public NavMeshSurface surface;

    // The last number of children the object had
    int childCount;

    void Start()
    {
        childCount = transform.childCount;
    }

    void Update()
    {
        // If we have no reference, do nothing
        if (!surface)
            return;

        // If our child count is not the same as before (i.e. a child has been added or removed)...
        if (childCount != transform.childCount)
        {
            // Bake the mesh
            surface.BuildNavMesh();

            // Update the last number of children we had
            childCount = transform.childCount;
        }
    }
}
