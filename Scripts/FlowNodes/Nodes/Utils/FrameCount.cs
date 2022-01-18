using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Utils/FrameCount", 524)]
    [NodeWidth(150)]
    public class FrameCount : MonoNode
    {
        [Output] public int frameCount;

        public override object GetValue(NodePort port)
        {
            return Time.frameCount;
        }

    }
}
