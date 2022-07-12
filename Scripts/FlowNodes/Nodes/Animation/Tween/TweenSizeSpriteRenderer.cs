using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/TweenSizeSpriteRenderer", 131)]
    public class TweenSizeSpriteRenderer : TweenObjectValue<SpriteRenderer, Vector2>
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
                Vector2 delta = targetValue - startValue;
                startValue += delta;
                targetValue += delta;

            }
        }

        protected override Vector2 GetStartValue()
        {
            return target.size;
        }

        protected override void SetValue(Vector2 value)
        {
            target.size = value;
        }

    }
}
