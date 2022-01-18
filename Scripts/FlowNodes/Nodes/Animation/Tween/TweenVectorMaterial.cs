using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/VectorMaterial", 129)]
    public class TweenVectorMaterial : TweenMaterial<Vector4>
    {
        protected override void OnTweenTick(float tNormal)
        {
            if (target == null)
            {
                return;
            }

            SetValue(Vector4.LerpUnclamped(startValue, targetValue, tNormal));
        }

        protected override void OnNextLoop(LoopType loopType)
        {
            if (loopType == LoopType.Incremental)
            {
                Vector4 delta = targetValue - startValue;
                startValue += delta;
                targetValue += delta;

            }
        }

        protected override Vector4 GetStartValue()
        {
            return target.GetVector(NamePort.GetInputValue(paramName));
        }

        protected override void SetValue(Vector4 value)
        {
            target.SetVector(Id, value);
        }

    }
}
