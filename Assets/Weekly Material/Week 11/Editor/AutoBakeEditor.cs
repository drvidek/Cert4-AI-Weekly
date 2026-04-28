using UnityEditor;
using UnityEngine;

public class AutoBakeNavMeshMenuItems
{
    // Reference the baker object live in the scene
    private static AutoBakeNavMesh baker;

    // a double is twice as many decimal places as a float
    // i.e. 'double' the precision
    private static double timeLastBake;

    #region Menu Items
    private static AutoBakeNavMesh CreateNewBaker()
    {
        // Create a new game object, called 'AutoBaker', with an AutoBakeNavMesh component attached
        GameObject bakerInScene = new GameObject("AutoBaker", typeof(AutoBakeNavMesh));

        // Tag the object as editor only for safety
        bakerInScene.tag = "EditorOnly";

        return bakerInScene.GetComponent<AutoBakeNavMesh>();
    }

    [MenuItem("Tools/Navigation/AutoBake/Start AutoBake")]
    public static void StartAutoBake()
    {
        // If there's no live baker object currently referenced...
        if (!baker)
        {
            // Look for one in the scene
            var bakerInScene = Object.FindAnyObjectByType<AutoBakeNavMesh>(FindObjectsInactive.Include);

            // If we found a baker in the scene, use that, else create a new one
            baker = bakerInScene ? bakerInScene : CreateNewBaker();
        }
        
        baker.onTick = Tick;

        baker.gameObject.SetActive(true);

        timeLastBake = EditorApplication.timeSinceStartup;
    }

    [MenuItem("Tools/Navigation/AutoBake/Stop AutoBake")]
    public static void StopAutoBake()
    {
        // DestroyImmediate should be used when in-editor, not Destroy()
        Object.DestroyImmediate(baker.gameObject);
    }

    [MenuItem("Tools/Navigation/AutoBake/Pause AutoBake")]
    public static void PauseAutoBake()
    {
        baker.gameObject.SetActive(false);
    }

    // 'true' here means this method is a MenuItem validator
    // the MenuItem listed at the path will disable if the validation does not pass
    [MenuItem("Tools/Navigation/AutoBake/Stop AutoBake", true)]
    [MenuItem("Tools/Navigation/AutoBake/Pause AutoBake", true)]    // We can stack Attributes to have more than one
    private static bool AutoBakerExists()
    {
        // Only enable the 'Stop' and 'Pause' menu item if the baker exists in the scene
        return baker;
    }

    private static void Tick()
    {
        // If the current time is greater than the last bake time plus the delay...
        if (EditorApplication.timeSinceStartup > timeLastBake + baker.secondsBetweenBake)
        {
            baker.Rebake();
            timeLastBake = EditorApplication.timeSinceStartup;
        }
    }

    #endregion
}
