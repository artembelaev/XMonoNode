using XMonoNode;

namespace XMonoNode
{
    public abstract class FlowNodeOut : MonoNode, IFlowNode
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

        public void TriggerFlow()
        {
            FlowUtils.FlowOutput(FlowOutputPort);
        }

        /// <summary>
        /// Handle input stream
        /// </summary>
        public abstract void Flow(NodePort flowPort);

        /// <summary>
        /// Stop execution of this flow node
        /// </summary>
        public virtual void Stop()
        {

        }

    }
}
