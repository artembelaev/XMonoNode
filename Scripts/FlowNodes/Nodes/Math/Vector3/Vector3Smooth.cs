using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ������������ ������� ��������� ���������
    /// </summary>
    [CreateNodeMenu("Vector3/Smooth", 31)]
    //[ExecuteInEditMode]
    public class Vector3Smooth : FlowNodeInOut, IUpdatable
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector3          Default = Vector3.zero;

        [Input(connectionType: ConnectionType.Override)]
        public Vector3          input;

        [Output]
        public Vector3          smooth;
        

        [Input(connectionType: ConnectionType.Override)]
        public float            lerpCoef = 5.0f;

        private NodePort DefaultPort;
        private NodePort inputPort;
        private NodePort smoothPort;
        private NodePort lerpCoefPort;

        private void Reset()
        {
            Name = "VectorSmooth (game only)";
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

            if (!Mathf.Approximately(Vector3.Distance(smooth, input), 0))
            {
                lerpCoef = lerpCoefPort.GetInputValue(lerpCoef);
                smooth = Vector3.Lerp(smooth, input, deltaTime * lerpCoef);
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
