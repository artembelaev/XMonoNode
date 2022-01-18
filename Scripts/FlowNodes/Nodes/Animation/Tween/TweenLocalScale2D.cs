using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/LocalScale2D", 114)]
    public class TweenLocalScale2D : TweenVector3RectTransform
    {

        private void Reset()
        {
            Name = "Local Scale 2D";
        }

        protected override Vector3 GetStartValue()
        {
            if (target == null)
            {
                return Vector3.one;
            }

            return target.localScale;
        }

        protected override void SetValue(Vector3 value)
        {
            target.localScale = value;
        }
    }
}
