using UnityEngine;

namespace XMonoNode
{
    public abstract class TweenColorGraphicBase : TweenObjectValue<UnityEngine.UI.Graphic, Color>
    {
        protected override void OnTweenTick(float tNormal)
        {
            if (target == null)
            {
                return;
            }
            SetValue(Color.LerpUnclamped(startValue, targetValue, tNormal));
        }

        protected override void OnNextLoop(LoopType loopType)
        {

            if (loopType == LoopType.Incremental)
            {
                Color delta = targetValue - startValue;
                startValue += delta;
                targetValue += delta;
            }
        }
    }
}
