using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Control/Switch Float Range", 18)]
    [NodeWidth(170)]
    public class SwitchFloatRange : SwitchRangeNodeBase<float>
    {
        private void Reset()
        {
            Ranges = new Range<float>[2] { new Range<float>(0, 60), new Range<float>(61, 120) };
        }
    }    
}
