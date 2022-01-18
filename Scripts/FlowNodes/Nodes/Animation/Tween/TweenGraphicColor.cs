using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Tween/GraphicColor", 117)]
    public class TweenGraphicColor : TweenColorGraphicBase
    {
        private void Reset()
        {
            Name = "Graphic Color";
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
