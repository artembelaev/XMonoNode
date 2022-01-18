using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetChildrenTextMeshProUGUI", 418)]
    [NodeWidth(190)]
    public class GetChildrenTextMeshProUGUI : GetComponentInChildren<TMPro.TextMeshProUGUI>
    {
        private void Reset()
        {
            Name = "Get Children TextMeshProUGUI";
        }
    }
}
