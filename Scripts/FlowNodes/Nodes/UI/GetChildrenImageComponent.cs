using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetChildrenImage", 416)]
    [NodeWidth(190)]
    public class GetChildrenImageComponent : GetComponentInChildren<Image>
    {
        private void Reset()
        {
            Name = "Get Children Image";
        }
    }
}
