using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/Pos Y", 24)]
    public class TweenPosY : TweenFloat<Transform>
    {
        private void Reset()
        {
            Name = "Pos Y";

        }
        protected override void Init()
        {
            base.Init();
#if UNITY_EDITOR
            targetValuePort.label = "Y";
#endif
        }

        protected override float GetStartValue()
        {
            return target.position.y;
        }

        protected override void SetValue(float value)
        {
            Vector3 pos = target.position;
            pos.y = value;
            target.position = pos;
        }
    }
}
