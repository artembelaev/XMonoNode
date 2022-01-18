using XMonoNode;

namespace XMonoNode
{
    public abstract class FlowNodeInOut : FlowNodeIn
    {
        [Output(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton]
        public Flow FlowOutput;

        private NodePort flowOutputPort;

        public NodePort FlowOutputPort => flowOutputPort;

        protected override void Init()
        {
            base.Init();

            flowOutputPort = GetOutputPort(nameof(FlowOutput));
            flowOutputPort.label = "Exit";
        }

        public void FlowOut()
        {
            FlowUtils.FlowOutput(FlowOutputPort);
        }
    }
}
