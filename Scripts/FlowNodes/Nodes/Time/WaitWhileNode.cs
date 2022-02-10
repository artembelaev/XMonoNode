using System.Threading.Tasks;
using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Wait While", 534)]
    public class WaitWhileNode : WaitBase, IUpdatable
    {
        private void Reset()
        {
            Name = "Wait While";
        }

        public override void Flow(NodePort flowPort)
        {
            triggered = true;
            OnUpdate(graph.DeltaTime);
        }

        public void OnUpdate(float deltaTime)
        {
            if (triggered && !Condition)
            {
                FlowUtils.FlowOutput(FlowOutputPort);
                triggered = false;
            }
        }

    }
}
