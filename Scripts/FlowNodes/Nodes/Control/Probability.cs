using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    [NodeWidth(160)]
    [CreateNodeMenu("Control/Probability", 22)]
    public class Probability : FlowNodeInOut
    {
        [Output, NodeInspectorButton] public Flow Else;

        [Range(0, 1)]
        [Input] public float probability;

        NodePort probabilityPort;
        NodePort ifPort;
        NodePort elsePort;

        protected override void Init()
        {
            base.Init();
            GetInputPort(nameof(FlowInput)).label = "Enter";

            probabilityPort = GetInputPort(nameof(probability));
            ifPort = GetOutputPort(nameof(FlowOutput));
            elsePort = GetOutputPort(nameof(Else));

            ifPort.label = "Yes";
            elsePort.label = "No";
        }

        public override void Flow(NodePort flowPort)
        {
            bool check = Random.Range(0f, 1f) < probabilityPort.GetInputValue(probability);
            NodePort output = check ? ifPort : elsePort;
            FlowUtils.FlowOutput(output);
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
