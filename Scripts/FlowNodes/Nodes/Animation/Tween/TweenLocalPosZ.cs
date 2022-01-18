using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Local Pos Z", 14)]
    public class TweenLocalPosZ : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Local Pos Z";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Local Z";
#endif
        }

        protected override float GetStartValue()
        {
            return target.localPosition.z;
        }

        protected override void SetValue(float value)
        {
            Vector3 pos = target.localPosition;
            pos.z = value;
            target.localPosition = pos;
        }
    }
}
