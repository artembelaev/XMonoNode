using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Control/" + nameof(RandomBranch), 23)]
    [NodeWidth(130)]
    public class RandomBranch : MonoNode, IFlowNode
    {
        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton]
        public Flow FlowInput;

        [Output(dynamicPortList: true, backingValue: ShowBackingValue.Always), FlowPort, NodeInspectorButton]
        public int[] FlowOutputWeights;

        private NodePort flowInputPort;

        public NodePort FlowInputPort
        {
            get => flowInputPort;
            set => flowInputPort = value;
        }

        private void Reset()
        {
            FlowOutputWeights = new int[] { 1, 1, 1 };
        }

        protected override void Init()
        {
            base.Init();

            flowInputPort = GetInputPort(nameof(FlowInput));
            flowInputPort.label = "Enter";

        }

        public void Flow(NodePort flowPort)
        {
            if (flowPort == flowInputPort)
            {
                if (FlowOutputWeights.Length <= 0)
                {
                    return;
                }

                int totalWeight = 0;
                for (int i = 0; i < FlowOutputWeights.Length; i++)
                {
                    totalWeight += Mathf.Abs(FlowOutputWeights[i]);
                }

                int randomValue = Random.Range(0, totalWeight) + 1;

                for (int i = 0; i < FlowOutputWeights.Length; i++)
                {
                    randomValue -= Mathf.Abs(FlowOutputWeights[i]);
                    if (randomValue <= 0)
                    {
                        FlowUtils.FlowOutput(GetOutputPort($"{nameof(FlowOutputWeights)} {i}"));
                        return;
                    }
                }
            }
        }

        public void TriggerFlow()
        {
            
        }

        public void Stop()
        {
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
