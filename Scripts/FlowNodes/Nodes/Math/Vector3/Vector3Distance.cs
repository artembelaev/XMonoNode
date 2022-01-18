using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Distance", 10)]
    [NodeWidth(150)]
    public class Vector3Distance : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3 a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3 b;

        [Output] public float distance;

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
            return Vector3.Distance(aPort.GetInputValue(a), bPort.GetInputValue(b));
        }
    }
}
