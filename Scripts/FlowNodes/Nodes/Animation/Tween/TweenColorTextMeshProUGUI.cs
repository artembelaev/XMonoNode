using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/TextMeshProUGUIColor", 120)]
    public class TweenColorTextMeshProUGUI : TweenColorTextMeshProUGUIBase
    {
        private void Reset()
        {
            Name = "TextMeshProUGUI Color";
        }

        protected override Color GetStartValue()
        {
            return target.color;
        }

        protected override void SetValue(Color value)
        {
            target.color = value;
        }
    }
}
