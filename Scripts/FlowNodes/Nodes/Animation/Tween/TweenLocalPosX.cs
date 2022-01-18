using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Local Pos X", 12)]
    public class TweenLocalPosX : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Local Pos X";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Local X";
#endif
        }

        protected override float GetStartValue()
        {
            return target.localPosition.x;
        }

        protected override void SetValue(float value)
        {
            Vector3 pos = target.localPosition;
            pos.x = value;
            target.localPosition = pos;
        }
    }
}
