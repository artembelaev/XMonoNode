using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/LocalPosition", 11)]
    public class TweenLocalPosition : TweenVector3Transform
    {

        private void Reset()
        {
            Name = "Local Position";
        }

        protected override Vector3 GetStartValue()
        {
            if (target == null)
            {
                return Vector3.zero;
            }

            return target.localPosition;
        }

        protected override void SetValue(Vector3 value)
        {
            if (target == null)
            {
                return;
            }

            target.localPosition = value;
        }
    }
}
