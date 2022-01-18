using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Local Scale Z", 34)]
    public class TweenLocalScaleZ : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Local Scale Z";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Local Scale Z";
#endif
        }

        protected override float GetStartValue()
        {
            return target.localScale.z;
        }

        protected override void SetValue(float value)
        {
            Vector3 scale = target.localScale;
            scale.z = value;
            target.localScale = scale;
        }
    }
}
