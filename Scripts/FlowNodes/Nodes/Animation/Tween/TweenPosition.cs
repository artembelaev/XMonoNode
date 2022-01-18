using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Position", 22)]
    public class TweenPosition : TweenVector3Transform
    {

        private void Reset()
        {
            Name = "Position";
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
            if (target == null)
            {
                return;
            }

            target.position = value;
        }
    }
}
