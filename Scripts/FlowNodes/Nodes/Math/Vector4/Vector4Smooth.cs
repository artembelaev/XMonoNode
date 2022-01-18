using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ќбеспечивает плавное изменение параметра
    /// </summary>
    [CreateNodeMenu("Vector4/Smooth", 31)]
    //[ExecuteInEditMode]
    [NodeWidth(150)]
    public class Vector4Smooth : FlowNodeInOut
    {

        [Input(connectionType: ConnectionType.Override)]
        public Vector4          Default = Vector3.zero;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4          input;

        [Output]
        public Vector4          smooth;
        

        [Input(connectionType: ConnectionType.Override)]
        public float            lerpCoef = 5.0f;

        private NodePort DefaultPort;
        private NodePort inputPort;
        private NodePort smoothPort;
        private NodePort lerpCoefPort;

        private void Reset()
        {
            Name = "Vector 4 Smooth";
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
            smooth = Default;
            FlowOut();
        }

        private void Update()
        {
            if (graph.UpdateMode == AnimatorUpdateMode.AnimatePhysics)
                return;

            ConditionalUpdate();
        }

        private void FixedUpdate()
        {
            if (graph.UpdateMode != AnimatorUpdateMode.AnimatePhysics)
                return;

            ConditionalUpdate();
        }

        private void ConditionalUpdate()
        {
            Default = DefaultPort.GetInputValue(Default);
            input = inputPort.GetInputValue(input);
            lerpCoef = lerpCoefPort.GetInputValue(lerpCoef);

            if (!Mathf.Approximately(Vector4.Distance(smooth, input), 0))
            {
                smooth = Vector4.Lerp(smooth, input, graph.DeltaTime * lerpCoef);
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
