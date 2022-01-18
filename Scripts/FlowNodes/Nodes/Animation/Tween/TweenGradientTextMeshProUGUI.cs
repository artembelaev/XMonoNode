using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/GradientTextMeshProUGUI", 125)]
    public class TweenGradientTextMeshProUGUI : TweenObjectValue<TextMeshProUGUI, VertexGradient>
    {
        protected override void OnTweenTick(float tNormal)
        {
            if (target == null)
            {
                return;
            }

            VertexGradient gradient = new VertexGradient();
            gradient.topLeft = Color.LerpUnclamped(startValue.topLeft, targetValue.topLeft, tNormal);
            gradient.topRight = Color.LerpUnclamped(startValue.topRight, targetValue.topRight, tNormal);
            gradient.bottomLeft = Color.LerpUnclamped(startValue.bottomLeft, targetValue.bottomLeft, tNormal);
            gradient.bottomRight = Color.LerpUnclamped(startValue.bottomRight, targetValue.bottomRight, tNormal);

            SetValue(gradient);
        }

        protected override void OnNextLoop(LoopType loopType)
        {
            if (loopType == LoopType.Incremental)
            {
                Color deltaTopLeft = targetValue.topLeft - startValue.topLeft;
                startValue.topLeft += deltaTopLeft;
                targetValue.topLeft += deltaTopLeft;

                Color deltaTopRight = targetValue.topRight - startValue.topRight;
                startValue.topRight += deltaTopRight;
                targetValue.topRight += deltaTopRight;

                Color deltaBottomLeft = targetValue.bottomLeft - startValue.bottomLeft;
                startValue.bottomLeft += deltaBottomLeft;
                targetValue.bottomLeft += deltaBottomLeft;

                Color deltaBottomRight = targetValue.bottomRight - startValue.bottomRight;
                startValue.bottomRight += deltaBottomRight;
                targetValue.bottomRight += deltaBottomRight;
            }
        }

        private void Reset()
        {
            Name = "Gradient TextMeshProUGUI";
        }

        protected override void Init()
        {
            base.Init();

            TargetValuePort.label = "Gradient";
        }

        protected override VertexGradient GetStartValue()
        {
            return target.colorGradient;
        }

        protected override void SetValue(VertexGradient value)
        {
            target.colorGradient = value;
        }

    }
}
