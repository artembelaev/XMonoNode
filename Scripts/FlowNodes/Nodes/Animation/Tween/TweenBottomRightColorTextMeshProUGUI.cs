using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/BottomRightColorTextMeshProUGUI", 124)]
    public class TweenBottomRightColorTextMeshProUGUI : TweenColorTextMeshProUGUIBase
    {
        private void Reset()
        {
            Name = "BottomRight Color TextMeshProUGUI";
        }

        protected override void Init()
        {
            base.Init();

            TargetValuePort.label = "BottomRight Color";
        }

        protected override Color GetStartValue()
        {
            return target.colorGradient.bottomRight;
        }

        protected override void SetValue(Color value)
        {
            VertexGradient gradient = target.colorGradient;
            gradient.bottomRight = value;
            target.colorGradient = gradient;
        }
    }
}
