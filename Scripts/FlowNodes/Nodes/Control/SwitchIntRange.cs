using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Control/Switch Int Range", 19)]
    [NodeWidth(170)]
    public class SwitchIntRange : SwitchRangeNodeBase<int>
    {
        private void Reset()
        {
            Ranges = new Range<int>[3] { new Range<int>(0, 10), new Range<int>(11, 20), new Range<int>(21, 30) };
        }
    }    
}
