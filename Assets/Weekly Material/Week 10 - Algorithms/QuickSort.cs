
public class QuickSort : SortContents
{
    protected override void UniqueSort()
    {
        loops = 0;
        DoQuickSort(0, contentToSort.Length - 1);
        print("Quick Sort took this many loops to complete: " + loops);
    }

    private int ReorderAndGetPivotIndex(int startIndex, int endIndex)
    {
        // Get the value of the end index
        int pivotValue = contentToSort[endIndex];

        // Start at the position to the left of the start index
        int lastSwapIndex = startIndex - 1;

        // For each position along the array,
        for (int i = startIndex; i < endIndex; i++)
        {
            // If the current value is larger than the pivot value...
            if (contentToSort[i] > pivotValue)
            {
                // Increase the position of the smaller numbers
                lastSwapIndex++;

                print($"{contentToSort[i]} is less than {pivotValue} so swapping it with {contentToSort[lastSwapIndex]}");

                // Swap the current number with the smaller
                Swap(i, lastSwapIndex);
            }
            else
            {
                print($"{contentToSort[i]} is greater than {pivotValue} so no swap needed.");
            }
        }

        Swap(lastSwapIndex + 1, endIndex);

        print($"Placed {contentToSort[lastSwapIndex + 1]} at index {lastSwapIndex + 1}, swapping with {contentToSort[endIndex]}");

        loops++;

        return lastSwapIndex + 1;
    }

    private void DoQuickSort(int startIndex, int endIndex)
    {
        print($"Current array: {string.Join(", ", contentToSort)}");
        
        print($"Sorting range from {startIndex} to {endIndex}");
        
        if (startIndex < endIndex)
        {
            int pivot = ReorderAndGetPivotIndex(startIndex, endIndex);

            print($"Found pivot at {pivot}");

            // This is called recursion - when a function calls itself
            // It's safe to do here because we have an 'if' condition which will eventually stop calling the function
            // This is similar to a 'while' loop
            DoQuickSort(startIndex, pivot - 1);
            DoQuickSort(pivot + 1, endIndex);
        }
        else
        {
            print("Invalid range. No action taken.");
        }
    }
}
