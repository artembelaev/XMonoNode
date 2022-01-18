using UnityEngine;

namespace XMonoNode
{
    public abstract class TweenVector2RectTransform: TweenObjectValue<RectTransform, Vector2>
    {
        protected override void OnTweenTick(float tNormal)
        {
            if (target == null)
            {
                return;
            }
            SetValue(Vector2.LerpUnclamped(startValue, targetValue, tNormal));
        }

        protected override void OnNextLoop(LoopType loopType)
        {
            if (loopType == LoopType.Incremental)
            {
                Vector2 delta = targetValue - startValue;
                startValue += delta;
                targetValue += delta;
            }
        }
    }
}
