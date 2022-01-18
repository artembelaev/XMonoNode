using UnityEngine;
using TMPro;

namespace XMonoNode
{
    public abstract class TweenColorTextMeshProUGUIBase : TweenObjectValue<TextMeshProUGUI, Color>
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
