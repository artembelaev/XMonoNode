using UnityEngine;

namespace XMonoNode
{
    public abstract class TweenVector3RectTransform: TweenObjectValue<RectTransform, Vector3>
    {
        protected override void OnTweenTick(float tNormal)
        {
            if (target == null)
            {
                return;
            }
            SetValue(Vector3.LerpUnclamped(startValue, targetValue, tNormal));
        }

        protected override void OnNextLoop(LoopType loopType)
        {
            if (loopType == LoopType.Incremental)
            {
                Vector3 delta = targetValue - startValue;
                startValue += delta;
                targetValue += delta;
            }
        }
    }
}
