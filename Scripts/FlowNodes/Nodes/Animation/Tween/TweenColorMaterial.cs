using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/ColorMaterial", 128)]
    public class TweenColorMaterial : TweenMaterial<Color>
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

        protected override Color GetStartValue()
        {
            return target.GetColor(NamePort.GetInputValue(paramName));
        }

        protected override void SetValue(Color value)
        {
            target.SetColor(Id, value);
        }

    }
}
