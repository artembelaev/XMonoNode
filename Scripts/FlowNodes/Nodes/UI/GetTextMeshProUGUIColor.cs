using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Get TextMeshProUGUI Color", 423)]
    [NodeWidth(220)]
    public class GetTextMeshProUGUIColor : GetObjectParameter<TextMeshProUGUI, Color>
    {
        protected override Color GetValue(TextMeshProUGUI obj)
        {
            return obj.color;
        }
    }
}
