using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/SetGraphicColor", 422)]
    [NodeWidth(190)]
    public class SetGraphicColor : SetObjectParameter<Graphic, Color>
    {
        protected override void SetValue(Graphic obj, Color value)
        {
            obj.color = value;
        }
    }
}
