using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/CanvasGroup Alpha", 118)]
    [AddComponentMenu("Animation/Tween/CanvasGroup Alpha", 118)]
    public class TweenCanvasGroupAlpha : TweenObjectValue<CanvasGroup, float>
    {
        private void Reset()
        {
            Name = "CanvasGroup Alpha";
        }

        protected override void Init()
        {
            base.Init();
            targetValuePort.label = "Alpha";
        }

        protected override float GetStartValue()
        {
            return target.alpha;
        }

        protected override void SetValue(float value)
        {
            target.alpha = value;
        }

        protected override void OnTweenTick(float tNormal)
        {
            if (target == null)
            {
                return;
            }
            SetValue(Mathf.LerpUnclamped(startValue, targetValue, tNormal));
        }

        protected override void OnNextLoop(LoopType loopType)
        {

            if (loopType == LoopType.Incremental)
            {
                float delta = targetValue - startValue;
                startValue += delta;
                targetValue += delta;
            }
        }
    }
}
