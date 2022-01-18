using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Dot", 7)]
    [NodeWidth(150)]
    public class Vector4Dot : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  a;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4  b;

        [Output] public Vector3 dot;

        private NodePort aPort;
        private NodePort bPort;

        protected override void Init()
        {
            base.Init();

            aPort = GetInputPort(nameof(a));
            bPort = GetInputPort(nameof(b));
        }

        public override object GetValue(NodePort port)
        {
            return Vector4.Dot(aPort.GetInputValue(a), bPort.GetInputValue(b));
        }
    }
}
