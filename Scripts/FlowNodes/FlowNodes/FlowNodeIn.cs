using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    public abstract class FlowNodeIn : MonoNode, IFlowNode
    {
        [Inline]
        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton]
        public Flow FlowInput;


        private NodePort flowInputPort;

        public NodePort FlowInputPort => flowInputPort;

        protected override void Init()
        {
            base.Init();

            flowInputPort = GetInputPort(nameof(FlowInput));

            flowInputPort.label = "Enter";
        }

        public virtual void TriggerFlow()
        {
            Flow(flowInputPort);
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
