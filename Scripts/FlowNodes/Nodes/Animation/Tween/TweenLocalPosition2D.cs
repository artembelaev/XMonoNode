using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/LocalPosition2D", 113)]
    public class TweenLocalPosition2D : TweenVector3RectTransform
    {

        private void Reset()
        {
            Name = "Local Position 2D";
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
            target.localPosition = value;
        }
    }
}
