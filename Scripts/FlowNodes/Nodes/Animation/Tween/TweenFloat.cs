using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    public abstract class TweenFloat<Obj>: TweenObjectValue<Obj, float> where Obj : UnityEngine.Object
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
    }
}
