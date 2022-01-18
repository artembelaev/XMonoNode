using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/TopRightColorTextMeshProUGUI", 121)]
    public class TweenTopRightColorTextMeshProUGUI : TweenColorTextMeshProUGUIBase
    {
        private void Reset()
        {
            Name = "TopRight Color TextMeshProUGUI";
        }

        protected override void Init()
        {
            base.Init();

            TargetValuePort.label = "TopRight Color";
        }

        protected override Color GetStartValue()
        {
            return target.colorGradient.topRight;
        }

        protected override void SetValue(Color value)
        {
            VertexGradient gradient = target.colorGradient;
            gradient.topRight = value;
            target.colorGradient = gradient;
        }
    }
}
