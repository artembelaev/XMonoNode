using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/Set TextMeshProUGUI Color", 424)]
    [NodeWidth(220)]
    public class SetTextMeshProUGUIColor : SetObjectParameter<TextMeshProUGUI, Color>
    {
        protected override void SetValue(TextMeshProUGUI obj, Color value)
        {
            obj.color = value;
        }
    }
}
