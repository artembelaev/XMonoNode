using System.Threading.Tasks;
using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Wait While", 534)]
    public class WaitWhileNode : WaitBase
    {
        private void Reset()
        {
            Name = "Wait While";
        }

        public override void Flow(NodePort flowPort)
        {
            triggered = true;
            CustomUpdate();
        }

        public override void CustomUpdate()
        {
            if (triggered && !Condition)
            {
                FlowUtils.FlowOutput(FlowOutputPort);
                triggered = false;
            }
        }

    }
}
