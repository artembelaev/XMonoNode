using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetImage", 415)]
    [NodeWidth(190)]
    public class GetImageComponent : GetComponentBase<Image>
    {
        private void Reset()
        {
            Name = "Get Image";
        }
    }
}
