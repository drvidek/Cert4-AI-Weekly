using UnityEngine;

public class BubbleUpSort : SortContents
{
    protected override void UniqueSort()
    {
        // Bubble up sort

        // Repeat the following, until no numbers are swapped:
            // Start at index 0.
            // If the current number is smaller than the next number,
                // Swap the two numbers.
            // Move to the next index, and compare the two numbers again.

        // To count how many times we had to work through the collection
        int loops = 0;

        // Whether or not two numbers were swapped at least once during this collection
        bool swapPerformed = true;

        // While we have performed a swap...
        while (swapPerformed)
        {
            // Flag the start of a new loop
            swapPerformed = false;

            for (int i = 0; i < contentToSort.Length - 1; i++)
            {
                // If the current number is smaller than the next number...
                if (contentToSort[i] < contentToSort[i + 1])
                {
                    // Swap the two numbers
                    Swap(i, i + 1);

                    // Flag a swap has been performed this loop
                    swapPerformed = true;
                }
                // Count up one loop
                loops++;
            }
        }

        // At the end, we'll see how many times the algorithm had to run to fully sort the list.
        print("Bubble Up sort took this many loops to complete: " + loops);
    }
}
