using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Local Scale Y", 33)]
    public class TweenLocalScaleY : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Local Scale Y";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Local Scale Y";
#endif
        }

        protected override float GetStartValue()
        {
            return target.localScale.y;
        }

        protected override void SetValue(float value)
        {
            Vector3 scale = target.localScale;
            scale.y = value;
            target.localScale = scale;
        }
    }
}
