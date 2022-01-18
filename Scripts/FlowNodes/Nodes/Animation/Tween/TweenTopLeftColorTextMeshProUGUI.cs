using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/TopLeftColorTextMeshProUGUI", 121)]
    public class TweenTopLeftColorTextMeshProUGUI : TweenColorTextMeshProUGUIBase
    {
        private void Reset()
        {
            Name = "TopLeft Color TextMeshProUGUI";
        }

        protected override void Init()
        {
            base.Init();

            TargetValuePort.label = "TopLeft Color";
        }

        protected override Color GetStartValue()
        {
            return target.colorGradient.topLeft;
        }

        protected override void SetValue(Color value)
        {
            VertexGradient gradient = target.colorGradient;
            gradient.topLeft = value;
            target.colorGradient = gradient;
        }
    }
}
