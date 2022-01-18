using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    [NodeWidth(130)]
    [CreateNodeMenu("Null/IfNull", 1011)]
    public class IfNull : FlowNodeInOut
    {
        [Inline]
        [Input(backingValue: ShowBackingValue.Never, connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None)]
        public Object value = null;

        [Output, NodeInspectorButton] public Flow Null;

        NodePort _objectPort;
        NodePort nullPort;

        protected override void Init()
        {
            base.Init();
            FlowOutputPort.label = "notNull";

            _objectPort = GetInputPort(nameof(value));
            nullPort = GetOutputPort(nameof(Null));
        }

        public override void Flow(NodePort flowPort)
        {
            NodePort output = _objectPort.GetInputValue(value) != null ? FlowOutputPort : nullPort;
            FlowUtils.FlowOutput(output);
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
