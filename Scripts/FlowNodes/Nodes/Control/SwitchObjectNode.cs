using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Control/Switch Object", 17)]
    [NodeWidth(200)]
    public class SwitchObjectNode : SwitchNodeBase<UnityEngine.Object>
    {}    
}
