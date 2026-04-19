using UnityEngine;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEditor.SceneManagement;

// This Attribute means the script will execute events like Start and Update in edit mode
[ExecuteAlways]
public class AutoBakeNavMesh : MonoBehaviour
{
    public float secondsBetweenBake = 20f;

    // A double is like a float but with twice as many decimal places
    // i.e. 'double' the precision of a float
    private double lastBakeTime;

    #region Menu Items
    ////// TEACHER! Do not write this section at first.
    /// Write the rest of the script and show it working in-editor first.
    /// Then discuss how it would be more convenient to be able to start and stop the auto-bake with menu items.
    
    private static GameObject bakerObject;

    /// <summary>
    /// Create a new game object with an AutoBakeNavMesh component attached.
    /// </summary>
    /// <returns></returns>
    private static GameObject CreateNewBaker()
    {
        // Create a new game object, named 'AutoBaker', with the AutoBakeNavMesh component attached. 
        GameObject baker = new GameObject("AutoBaker", typeof(AutoBakeNavMesh));

        // Mark the object as EditorOnly for build safety
        baker.tag = "EditorOnly";

        return baker;
    }

    [MenuItem("Tools/AI/AutoBake NavMesh/Start")]
    public static void StartAutoBake()
    {
        // If we don't have a reference to our single baker object...
        if (!bakerObject)
        {
            // Look for an existing baker object in the scene
            var baker = FindAnyObjectByType<AutoBakeNavMesh>(FindObjectsInactive.Include);

            // If we found an AutoBakeNavMesh component, use its game object as the baker object. Else, create a new baker.
            bakerObject = baker ? baker.gameObject : CreateNewBaker();
        }

        // Ensure the baker object is enabled
        bakerObject.SetActive(true);
    }

    [MenuItem("Tools/AI/AutoBake NavMesh/Pause")]
    public static void PauseAutoBake()
    {
        bakerObject.SetActive(false);
    }

    // By passing 'true' here, we mark this method as a Menu Item validator
    // The method should return true/false to enable/disable the menu item at the path given
    [MenuItem("Tools/AI/AutoBake NavMesh/Pause", true)]
    static bool PauseAutoBakeValidate()
    {
        return bakerObject;
    }

    [MenuItem("Tools/AI/AutoBake NavMesh/Stop")]
    public static void StopAutoBake()
    {
        // DestroyImmediate should be used when in-editor
        DestroyImmediate(bakerObject);
    }

    [MenuItem("Tools/AI/AutoBake NavMesh/Stop", true)]
    static bool StopAutoBakeValidate()
    {
        return bakerObject;
    }

    #endregion

    // By using OnEnable, we connect our timer behaviour to the game object being enabled or disabled
    void OnEnable()
    {
        // Get the current time
        lastBakeTime = EditorApplication.timeSinceStartup;
        Rebake();
        print("AutoBake started.");
    }

    void Update()
    {
        // If we're in live gameplay, do nothing
        if (Application.isPlaying)
            return;

        // If the current time is longer than the previous bake time plus the delay between bakes...
        if (EditorApplication.timeSinceStartup > lastBakeTime + secondsBetweenBake)
        {
            // We should bake
            Rebake();

            // Reset the last baked time
            lastBakeTime = EditorApplication.timeSinceStartup;
        }
    }

    void OnDisable()
    {
        print("AutoBake paused.");
    }

    void Rebake()
    {
        print("AutoBake activating.");
        
        foreach (NavMeshSurface surface in FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None))
        {
            surface.BuildNavMesh();
        }

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    void OnDestroy()
    {
        print("AutoBake stopped.");
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
}
