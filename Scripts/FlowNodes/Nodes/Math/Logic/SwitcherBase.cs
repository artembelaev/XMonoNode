namespace XMonoNode
{
    public abstract class SwitcherBase : MonoNode, IFlowNode
    {
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never, typeConstraint: TypeConstraint.Inherited), Inline]
        public bool input;

        [Output]
        public bool output = false;

        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), Hiding, NodeInspectorButton]
        public Flow reset;

        protected NodePort inputPort = null;


        private NodePort resetPort;

        public NodePort ResetPort => resetPort;

        public virtual void Flow(NodePort flowPort)
        {
            output = false;
        }


        public override object GetValue(NodePort port)
        {
            return output;
        }

        public void Stop()
        {
        }

        public virtual void TriggerFlow()
        {
            Flow(resetPort);
        }

        protected override void Init()
        {
            base.Init();

            resetPort = GetInputPort(nameof(reset));
            inputPort = GetInputPort(nameof(input));
        }
    }
}