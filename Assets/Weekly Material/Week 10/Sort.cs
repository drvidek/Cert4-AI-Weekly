using UnityEngine;

public abstract class SortContents : MonoBehaviour
{
    public int[] contentToSort;

    /// <summary>
    /// Randomise the contents of the component.
    /// </summary>
    [ContextMenu("Scramble")]  // ContextMenu attribute allows you to run this function by right-clicking on the component in the scene.
    public void Scramble()
    {
        // For as big as our list is,
        for (int i = 0; i < contentToSort.Length; i++)
        {
            // Get a random position in the list
            int randomIndex = Random.Range(0, contentToSort.Length);

            // Swap that position with the current position
            Swap(i, randomIndex);
        }
    }

    protected abstract void UniqueSort();

    // We can't use ContextMenu on an abstract method, so we'll wrap it in another method.
    // This means we don't have to add the attribute in every child script.
    [ContextMenu("Sort")]
    public void Sort()
    {
        UniqueSort();
    }

    /// <summary>
    /// Swaps the items at the two indexes provided.
    /// </summary>
    /// <param name="positionA"></param>
    /// <param name="positionB"></param>
    public void Swap(int positionA, int positionB)
    {
        int temp = contentToSort[positionA];
        contentToSort[positionA] = contentToSort[positionB];
        contentToSort[positionB] = temp;
    }
}
