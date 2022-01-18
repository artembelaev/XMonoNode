using System.Threading.Tasks;
using UnityEngine;

namespace XMonoNode
{
    [NodeWidth(150)]
    public abstract class WaitBase : FlowNodeInOut
    {
        [Input] public bool condition;

        protected bool triggered = false;
        private NodePort conditionPort = null;
        protected bool Condition
        {
            get
            {
                return conditionPort.GetInputValue(condition);
            }
        }

        protected override void Init()
        {
            base.Init();
            conditionPort = GetInputPort(nameof(condition));
            FlowInputPort.label = "Enter";
            FlowOutputPort.label = "Exit";
        }

        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

        public override void Stop()
        {
            base.Stop();
            triggered = false;
        }

    }
}
