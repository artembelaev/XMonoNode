using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/Destroy Object", 402)]
    [AddComponentMenu("GameObject/Destroy Object", 402)]
    [NodeWidth(170)]
    public class DestroyObject : FlowNodeInOut
    {
        [Input]
        public GameObject Target;

        public override void Flow(NodePort flowPort)
        {
            var toDestroy = GetInputValue(nameof(Target), Target);
            Destroy(toDestroy);
            FlowOut();
        }

        public override object GetValue(NodePort port) {
            return null;
        }
    }
}
