using UnityEngine.Events;
using XMonoNode;

namespace XMonoNode
{
    [NodeWidth(350)]
    [CreateNodeMenu("Action/Invoke Event", 14)]
    public class InvokeEvent : FlowNodeInOut
    {
        public UnityEvent Target;

        private void Reset()
        {
            Name = "Invoke Event";
        }

        public override void Flow(NodePort flowPort)
        {
            Target.Invoke();
            FlowOut();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
