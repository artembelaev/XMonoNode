using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Diff", 4)]
    [NodeWidth(150)]
    public class Vector3Diff : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3 a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3 b;

        [Output] public Vector3 difference;

        private NodePort aPort;
        private NodePort bPort;

        protected override void Init()
        {
            base.Init();

            aPort = GetInputPort(nameof(a));
            bPort = GetInputPort(nameof(b));

            NodePort portOut = GetOutputPort(nameof(difference));
            if (portOut != null)
            {
                portOut.label = "A - B";
            }
        }

        public override object GetValue(NodePort port)
        {
            return aPort.GetInputValue(a) - bPort.GetInputValue(b);
        }
    }
}
