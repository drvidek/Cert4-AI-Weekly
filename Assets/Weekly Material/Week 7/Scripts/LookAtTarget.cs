using UnityEngine;

//////// TEACHER! Namespace only needed to distinguish in this master project
namespace Week7
{
    public class LookAtTarget : HasTarget
    {
        public void Update()
        {
            LookAt();
        }

        /// <summary>
        /// Look directly at the target immediately.
        /// </summary>
        public virtual void LookAt()
        {
            transform.LookAt(target);
        }
    }
}