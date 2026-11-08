using UnityEngine;

public abstract class SortContents : MonoBehaviour
{

    public int[] contentToSort;

    // Track how many times we performed our "sort" operation
    protected int loops;

    /// <summary>
    /// Fill the existing array with new random numbers.
    /// </summary>
    [ContextMenu("Fill Randomly")]  // ContextMenu attribute allows you to run this function by right-clicking on the component in the scene.
    public void FillRandom()
    {
        for (int i = 0; i < contentToSort.Length; i++)
        {
            contentToSort[i] = Random.Range(-200, 201);
        }
    }

    /// <summary>
    /// Randomise the contents of the component.
    /// </summary>
    [ContextMenu("Scramble")]
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

    /// <summary>
    /// Override to implement a different sorting algorithm for each solution.
    /// </summary>
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
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    public void Swap(int indexA, int indexB)
    {
        // Store the first value
        int temp = contentToSort[indexA];

        // Replace the first value with the second value
        contentToSort[indexA] = contentToSort[indexB];

        // Replace the second value with the stored first value
        contentToSort[indexB] = temp;
    }

    ///////////// Below is a solution for the challenge.
    /// Do not write this code until next week when reviewing the challenge.
    
    public enum Order
    {
        LargestFirst,
        SmallestFirst
    }

    public Order order;

    // Replace the 'if a >/< b' statements with 'if Compare(a, b)'  in:
        // BUBBLE - line 22
        // QUICK - line 24
    public bool Compare(int valueA, int valueB)
    {
        return order == Order.LargestFirst ? valueA < valueB : valueA > valueB;
    }
}
