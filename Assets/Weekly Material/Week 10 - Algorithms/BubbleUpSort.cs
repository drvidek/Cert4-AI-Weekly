
public class BubbleUpSort : SortContents
{
    protected override void UniqueSort()
    {
        loops = 0;

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
                    print($"{contentToSort[i]} is smaller than {contentToSort[i + 1]} so swapping these numbers.");

                    // Swap the two numbers
                    Swap(i, i + 1);

                    // Flag a swap has been performed this loop
                    swapPerformed = true;
                }
                // Count up one loop
                loops++;
            }

            print($"Current array: {string.Join(", ", contentToSort)}");
        }

        // At the end, we'll see how many times the algorithm had to run to fully sort the list.
        print("Bubble Up sort took this many loops to complete: " + loops);
    }
}
