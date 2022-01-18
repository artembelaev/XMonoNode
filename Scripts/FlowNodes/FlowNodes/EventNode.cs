using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    public abstract class EventNode : MonoNode, IFlowNode
    {
        [Output, NodeInspectorButton(NodeInspectorButtonShow.Always)] public Flow FlowOutput;

        private NodePort flowOutputPort;

        public NodePort FlowOutputPort
        {
            get => flowOutputPort;
            set => flowOutputPort = value;
        }

        protected override void Init()
        {
            base.Init();
            FlowOutputPort = GetOutputPort(nameof(FlowOutput));
        }

        public virtual void TriggerFlow()
        {
            FlowUtils.FlowOutput(FlowOutputPort);
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public virtual void Flow(NodePort flowPort)
        {
            FlowUtils.FlowOutput(FlowOutputPort);
        }

        public virtual void Stop()
        {
            // dummy
        }
    }
}
