using System.Threading.Tasks;
using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Wait Until", 533)]
    public class WaitUntilNode : WaitBase
    {
        private void Reset()
        {
            Name = "Wait Until";
        }

        public override void Flow(NodePort flowPort)
        {
            triggered = true;
            CustomUpdate();
        }

        public override void CustomUpdate()
        {
            if (triggered && Condition)
            {
                FlowUtils.FlowOutput(FlowOutputPort);
                triggered = false;
            }
        }

    }
}
