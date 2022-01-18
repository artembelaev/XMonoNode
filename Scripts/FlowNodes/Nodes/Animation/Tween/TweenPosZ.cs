using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Pos Z", 25)]
    public class TweenPosZ : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Pos Z";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Z";
#endif
        }

        protected override float GetStartValue()
        {
            return target.position.z;
        }

        protected override void SetValue(float value)
        {
            Vector3 pos = target.position;
            pos.z = value;
            target.position = pos;
        }
    }
}
