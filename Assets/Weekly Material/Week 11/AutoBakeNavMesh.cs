using UnityEngine;
using Unity.AI.Navigation;
using System;

// The script will execute events like Update in edit mode
[ExecuteAlways]
public class AutoBakeNavMesh : MonoBehaviour
{
    public float secondsBetweenBake = 20f;

    // an Action is like a UnityEvent but it belongs to C#, in 'using System'
    public Action onTick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Rebake();
    }

    // Update is called once per frame
    void Update()
    {
        // If we're in gameplay, do nothing
        if (Application.isPlaying)
        {
            return;
        }

        // ? here makes sure there is a listener to the event
        // C# Actions will throw a null error if no listeners and no ?
        onTick?.Invoke();
    }

    public void Rebake()
    {
        print("AutoBake activating.");

        NavMeshSurface[] surfaces = FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);

        foreach (NavMeshSurface surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }

    private void OnDisable()
    {
        print("AutoBake paused.");
    }

    private void OnDestroy()
    {
        print("AutoBake stopped.");
    }
}
