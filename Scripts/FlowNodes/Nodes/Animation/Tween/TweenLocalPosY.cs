using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Local Pos Y", 13)]
    public class TweenLocalPosY : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Local Pos Y";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Local Y";
#endif
        }

        protected override float GetStartValue()
        {
            return target.localPosition.y;
        }

        protected override void SetValue(float value)
        {
            Vector3 pos = target.localPosition;
            pos.y = value;
            target.localPosition = pos;
        }
    }
}
