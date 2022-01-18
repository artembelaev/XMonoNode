using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetChildrenGraphic", 411)]
    [NodeWidth(190)]
    public class GetChildrenGraphicComponent : GetComponentInChildren<Graphic>
    {
        private void Reset()
        {
            Name = "Get Children Graphic";
        }
    }
}
