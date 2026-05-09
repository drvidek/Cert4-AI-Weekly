using UnityEngine;

public class RandomiseAttribute : PropertyAttribute
{
    private float[] min = new float[] { float.MinValue, float.MinValue, float.MinValue, float.MinValue };
    private float[] max = new float[] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue };
    public readonly int rangeCount;

    /// <summary>
    /// Get a random int. Optionally provide an index to get within a min/max if available.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int Int(int index = -1)
    {
        return Random.Range(MinInt(index), MaxInt(index));
    }

    /// <summary>
    /// Get a random float. Optionally provide an index to get within a min/max if available.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float Float(int index = -1)
    {
        return Random.Range(MinFloat(index), MaxFloat(index));
    }

    /// <summary>
    /// Get a random float between 0-1. Optionally provide an index to get within a normalised min/max if available.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float Float01(int index = -1)
    {
        float f = Float(index);
        return Mathf.InverseLerp(MinFloat(index), MaxFloat(index), f);
    }

    /// <summary>
    /// Get the minimum float value. Optionally provide an index to get the minimum of a specific range.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float MinFloat(int index = -1)
    {
        if (index < 0 || index >= rangeCount)
        {
            return float.MinValue;
        }
        return min[index];
    }

    /// <summary>
    /// Get the maximum float value. Optionally provide an index to get the maximum of a specific range.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public float MaxFloat(int index = -1)
    {
        if (index < 0 || index >= rangeCount)
        {
            return float.MaxValue;
        }
        return max[index];
    }

    /// <summary>
    /// Get the minimum int value. Optionally provide an index to get the minimum of a specific range.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int MinInt(int index = -1)
    {
        if (index < 0 || index >= rangeCount)
        {
            return int.MinValue;
        }
        return (int)min[index];
    }

    /// <summary>
    /// Get the maximum float value. Optionally provide an index to get the maximum of a specific range.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int MaxInt(int index = -1)
    {
        if (index < 0 || index >= rangeCount)
        {
            return int.MaxValue;
        }
        return (int)max[index];
    }

    public RandomiseAttribute()
    {
        rangeCount = 0;
    }

    /// <summary>
    /// Randomise an int, float, Color hue, or Vector.x within a given range by clicking a button.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public RandomiseAttribute(float min, float max)
    {
        this.min[0] = min;
        this.max[0] = max;
        rangeCount = 1;
    }

    /// <summary>
    /// Randomise a Color hue and saturation or Vector.xy within a given range by clicking a button.
    /// </summary>
    /// <param name="min0"></param>
    /// <param name="max0"></param>
    /// <param name="min1"></param>
    /// <param name="max1"></param>
    public RandomiseAttribute(float min0, float max0, float min1, float max1)
    {
        min[0] = min0;
        min[1] = min1;
        max[0] = max0;
        max[1] = max1;
        rangeCount = 2;
    }

    /// <summary>
    /// Randomise a Color HSV or Vector.xyz within a given range by clicking a button.
    /// </summary>
    /// <param name="min0"></param>
    /// <param name="max0"></param>
    /// <param name="min1"></param>
    /// <param name="max1"></param>
    /// <param name="min2"></param>
    /// <param name="max2"></param>
    public RandomiseAttribute(float min0, float max0, float min1, float max1, float min2, float max2)
    {
        min[0] = min0;
        min[1] = min1;
        min[2] = min2;
        max[0] = max0;
        max[1] = max1;
        max[2] = max2;
        rangeCount = 3;
    }

    /// <summary>
    /// Randomise a Quaternion within a given range by clicking a button.
    /// </summary>
    /// <param name="min0"></param>
    /// <param name="max0"></param>
    /// <param name="min1"></param>
    /// <param name="max1"></param>
    /// <param name="min2"></param>
    /// <param name="max2"></param>
    public RandomiseAttribute(float min0, float max0, float min1, float max1, float min2, float max2, float min3, float max3)
    {
        min[0] = min0;
        min[1] = min1;
        min[2] = min2;
        min[3] = min3;
        max[0] = max0;
        max[1] = max1;
        max[2] = max2;
        max[3] = max3;
        rangeCount = 4;
    }
}
