using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Dot", 7)]
    [NodeWidth(150)]
    public class Vector3Dot : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  b;

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
            return Vector3.Dot(aPort.GetInputValue(a), bPort.GetInputValue(b));
        }
    }
}
