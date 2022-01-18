using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetChildrenButton", 414)]
    [NodeWidth(190)]
    public class GetChildrenButtonComponent : GetComponentInChildren<Button>
    {
        private void Reset()
        {
            Name = "Get Children Button";
        }
    }
}
