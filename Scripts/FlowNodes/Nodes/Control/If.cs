using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    [NodeWidth(175)]
    [CreateNodeMenu("Control/" + nameof(If), 11)]
    public class If : FlowNodeInOut
    {
        [Inline]
        [Input, NodeInspectorButton] public bool condition;
        [Output, NodeInspectorButton] public Flow Else;

        NodePort conditionPort;
        NodePort ifPort;
        NodePort elsePort;

        protected override void Init()
        {
            base.Init();
            GetInputPort(nameof(FlowInput)).label = "Enter";

            conditionPort = GetInputPort(nameof(condition));
            ifPort = GetOutputPort(nameof(FlowOutput));
            elsePort = GetOutputPort(nameof(Else));

            ifPort.label = "if (condition)";
            elsePort.label = "else";
        }

        public override void Flow(NodePort flowPort)
        {
            NodePort output = conditionPort.GetInputValue(condition) ? ifPort : elsePort;
            FlowUtils.FlowOutput(output);
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
