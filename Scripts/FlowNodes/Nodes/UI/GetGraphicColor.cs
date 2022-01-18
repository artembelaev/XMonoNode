using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetGraphicColor", 421)]
    [NodeWidth(190)]
    public class GetGraphicColor : GetObjectParameter<Graphic, Color>
    {
        protected override Color GetValue(Graphic obj)
        {
            return obj.color;
        }
    }
}
