using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/FloatMaterial", 127)]
    public class TweenFloatMaterial : TweenMaterial<float>
    {
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

        protected override float GetStartValue()
        {
            return target.GetFloat(NamePort.GetInputValue(paramName));
        }

        protected override void SetValue(float value)
        {
            target.SetFloat(Id, value);
        }

    }
}
