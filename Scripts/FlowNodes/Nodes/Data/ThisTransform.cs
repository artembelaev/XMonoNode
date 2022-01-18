using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/This", 440)]
    [NodeWidth(140)]
    public class ThisTransform : MonoNode
    {
        [Output] public Transform output;

        private void Reset()
        {
            Name = "This";
        }

        public override object GetValue(NodePort port)
        {
            return gameObject.transform;
        }
    }
}
