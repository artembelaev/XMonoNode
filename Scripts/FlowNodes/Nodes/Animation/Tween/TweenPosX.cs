using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Pos X", 23)]
    public class TweenPosX : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Pos X";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "X";
#endif
        }

        protected override float GetStartValue()
        {
            return target.position.x;
        }

        protected override void SetValue(float value)
        {
            Vector3 pos = target.position;
            pos.x = value;
            target.position = pos;
        }
    }
}
