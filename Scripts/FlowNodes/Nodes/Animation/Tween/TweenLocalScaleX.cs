using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Local Scale X", 32)]
    public class TweenLocalScaleX : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Local Scale X";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Local Scale X";
#endif
        }

        protected override float GetStartValue()
        {
            return target.localScale.x;
        }

        protected override void SetValue(float value)
        {
            Vector3 scale = target.localScale;
            scale.x = value;
            target.localScale = scale;
        }
    }
}
