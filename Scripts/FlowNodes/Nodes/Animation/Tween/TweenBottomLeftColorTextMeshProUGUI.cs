using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/BottomLeftColorTextMeshProUGUI", 123)]
    public class TweenBottomLeftColorTextMeshProUGUI : TweenColorTextMeshProUGUIBase
    {
        private void Reset()
        {
            Name = "BottomLeft Color TextMeshProUGUI";
        }

        protected override void Init()
        {
            base.Init();

            TargetValuePort.label = "BottomLeft Color";
        }

        protected override Color GetStartValue()
        {
            return target.colorGradient.bottomLeft;
        }

        protected override void SetValue(Color value)
        {
            VertexGradient gradient = target.colorGradient;
            gradient.bottomLeft = value;
            target.colorGradient = gradient;
        }
    }
}
