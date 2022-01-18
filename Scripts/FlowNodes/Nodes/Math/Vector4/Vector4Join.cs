using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Join", 1)]
    [NodeWidth(150)]
    public class Vector4Join : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public float x;

        [Input(connectionType: ConnectionType.Override)]
        public float y;

        [Input(connectionType: ConnectionType.Override)]
        public float z;

        [Input(connectionType: ConnectionType.Override)]
        public float w;

        [Output] public Vector4 vector4;

        private NodePort xPort;
        private NodePort yPort;
        private NodePort zPort;
        private NodePort wPort;

        protected override void Init()
        {
            base.Init();
            xPort = GetInputPort(nameof(x));
            yPort = GetInputPort(nameof(y));
            zPort = GetInputPort(nameof(z));
            wPort = GetInputPort(nameof(w));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return new Vector4(xPort.GetInputValue(x), yPort.GetInputValue(y), zPort.GetInputValue(z), wPort.GetInputValue(w));
        }
    }
}
