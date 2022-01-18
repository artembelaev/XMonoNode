using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Position2D", 113)]
    public class TweenPosition2D : TweenVector3RectTransform
    {

        private void Reset()
        {
            Name = "Position 2D";
        }

        protected override Vector3 GetStartValue()
        {
            if (target == null)
            {
                return Vector3.zero;
            }

            return target.position;
        }

        protected override void SetValue(Vector3 value)
        {
            target.position = value;
        }
    }
}
