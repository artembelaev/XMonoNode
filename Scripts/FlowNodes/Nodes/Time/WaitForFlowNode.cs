using System.Threading.Tasks;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Wait For Flow", 535)]
    [NodeWidth(160)]
    public class WaitForFlowNode : FlowNodeInOut
    {
        [Input, NodeInspectorButton] public Flow flow;

        private bool triggered = false;

        private NodePort enterPort;
        private NodePort onFlowPort;
        private NodePort exitPort;

        private void Reset()
        {
            Name = "Wait For Flow";
        }

        protected override void Init()
        {
            base.Init();
            enterPort = GetInputPort(nameof(FlowInput));
            onFlowPort = GetInputPort(nameof(flow));
            exitPort = GetOutputPort(nameof(FlowOutput));

            enterPort.label = "Enter";
            exitPort.label = "Exit";
        }

        public override void Flow(NodePort flowPort)
        {
            if (flowPort == enterPort)
            {
                triggered = true;
            }
            else if (flowPort == onFlowPort && triggered)
            {
                FlowOut();
                triggered = false;
            }
        }

        // Return the correct value of an output port when requested
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
