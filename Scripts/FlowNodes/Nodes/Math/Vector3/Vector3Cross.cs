using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Cross", 13)]
    [NodeWidth(150)]
    public class Vector3Cross : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  b;

        [Output] public Vector3 cross;

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
            return Vector3.Cross(aPort.GetInputValue(a), bPort.GetInputValue(b));
        }
    }
}
