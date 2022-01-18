using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/LocalScale", 31)]
    public class TweenLocalScale : TweenVector3Transform
    {
        private void Reset()
        {
            Name = "Local Scale";
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
            if (target == null)
            {
                return;

            }

            target.localScale = value;
        }
    }
}
