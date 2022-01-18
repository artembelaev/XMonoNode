using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Equals", 14)]
    [NodeWidth(150)]
    public class Vector4Equals : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4  a;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4  b;

        [Output] public bool equals;

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
            return Equals(aPort.GetInputValue(a), bPort.GetInputValue(b));
        }
    }
}
