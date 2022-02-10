using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ќбеспечивает плавное изменение параметра
    /// </summary>
    [CreateNodeMenu("Float/Smooth", -163)]
    [NodeWidth(190)]
    public class FloatSmooth : FlowNodeInOut, IUpdatable
    {

        [Input(connectionType: ConnectionType.Override)]
        public float            Default = 0.0f;

        [Inline]
        [Input(connectionType: ConnectionType.Override)]
        public float            input = 0.0f;
        [Output]
        public float            lerpOutput = 0.0f;

        [SerializeField, Hiding]
        private float           inputDelta = -0.01f;
        

        [Input(connectionType: ConnectionType.Override)]
        public float            lerpUp = 10000.0f;
        [Input(connectionType: ConnectionType.Override)]
        public float            lerpDown = 5.0f;
        
        private NodePort DefaultPort;
        private NodePort inputPort;
        private NodePort outputPort;
        private NodePort lerpUpPort;
        private NodePort lerpDownPort;

        private void Reset()
        {
            Name = "FloatSmooth";
        }

        protected override void Init()
        {
            base.Init();
            DefaultPort = GetInputPort(nameof(Default));
            inputPort = GetInputPort(nameof(input));
            outputPort = GetOutputPort(nameof(lerpOutput));
            lerpUpPort = GetInputPort(nameof(lerpUp));
            lerpDownPort = GetInputPort(nameof(lerpDown));

            NodePort flowInputPort = GetInputPort(nameof(FlowInput));
            if (flowInputPort != null)
            {
                flowInputPort.label = "Set Default";
            }

            outputPort.label = "Output";
        }

        public override void Flow(NodePort flowPort)
        {
            lerpOutput = DefaultPort.GetInputValue(Default);
            FlowOut();
        }

        public virtual void OnUpdate(float deltaTime)
        {
            input = inputPort.GetInputValue(input);

            if (inputDelta < 0f && !Mathf.Approximately(lerpOutput, input) ||
                Mathf.Abs(lerpOutput - input) < inputDelta)
            {
                lerpUp = lerpUpPort.GetInputValue(lerpUp);
                lerpDown = lerpDownPort.GetInputValue(lerpDown);
                lerpOutput = Mathf.Lerp(lerpOutput, input, deltaTime * (input > lerpOutput ? lerpUp : lerpDown));
            }
        }

        public override object GetValue(NodePort port)
        {
            return lerpOutput;
        }
    }
}
