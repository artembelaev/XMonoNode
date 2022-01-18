using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Equals", 14)]
    [NodeWidth(150)]
    public class Vector3Equals : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  b;

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
