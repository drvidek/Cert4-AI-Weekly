using UnityEngine;

namespace Custom {
    
public class RangeAttribute : PropertyAttribute
{
    public float min, max;

    public RangeAttribute(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

}