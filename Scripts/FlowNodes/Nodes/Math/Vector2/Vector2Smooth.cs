using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ������������ ������� ��������� ���������
    /// </summary>
    [CreateNodeMenu("Vector2/Smooth", 31)]
    //[ExecuteInEditMode]
    [NodeWidth(135)]
    public class Vector2Smooth : FlowNodeInOut, IUpdatable
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector2          Default = Vector2.zero;

        [Input(connectionType: ConnectionType.Override)]
        public Vector2          input;

        [Output]
        public Vector2          smooth;
        

        [Input(connectionType: ConnectionType.Override)]
        public float            lerpCoef = 5.0f;

        private NodePort DefaultPort;
        private NodePort inputPort;
        private NodePort smoothPort;
        private NodePort lerpCoefPort;

        private void Reset()
        {
            Name = "VectorSmooth";
        }

        protected override void Init()
        {
            base.Init();
            NodePort flowInputPort = GetInputPort(nameof(FlowInput));
            if (flowInputPort != null)
            {
                flowInputPort.label = "Set Default";
            }

            DefaultPort  = GetInputPort(nameof(Default));
            inputPort    = GetInputPort(nameof(input));
            smoothPort  = GetOutputPort(nameof(smooth));
            lerpCoefPort = GetInputPort(nameof(lerpCoef));
        }

        public override void Flow(NodePort flowPort)
        {
            smooth = DefaultPort.GetInputValue(Default);
            FlowOut();
        }

        public virtual void OnUpdate(float deltaTime)
        {
            input = inputPort.GetInputValue(input);

            if (!Mathf.Approximately(Vector2.Distance(smooth, input), 0))
            {
                lerpCoef = lerpCoefPort.GetInputValue(lerpCoef);
                smooth = Vector2.Lerp(smooth, input, deltaTime * lerpCoef);
            }
        }

        public override object GetValue(NodePort port)
        {
            if (port == smoothPort)
            {
                return smooth;
            }
            else
                return null;
        }
    }
}
