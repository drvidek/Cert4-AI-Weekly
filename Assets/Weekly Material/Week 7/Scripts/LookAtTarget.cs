using UnityEngine;

// Namespace only needed to distinguish in this master project
namespace Week7
{
    public class LookAtTarget : HasTarget
    {
        public void Update()
        {
            LookAt();
        }

        public void LookAt()
        {
            transform.LookAt(target);
        }

        public void SetAngles(Vector3 newEulerAngles)
        {
            transform.localEulerAngles = newEulerAngles;
        }
    }
}