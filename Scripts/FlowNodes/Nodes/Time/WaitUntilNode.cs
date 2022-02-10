using System.Threading.Tasks;
using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Wait Until", 533)]
    public class WaitUntilNode : WaitBase, IUpdatable
    {
        private void Reset()
        {
            Name = "Wait Until";
        }

        public override void Flow(NodePort flowPort)
        {
            triggered = true;
            OnUpdate(graph.DeltaTime);
        }

        public void OnUpdate(float deltaTime)
        {
            if (triggered && Condition)
            {
                FlowUtils.FlowOutput(FlowOutputPort);
                triggered = false;
            }
        }

    }
}
