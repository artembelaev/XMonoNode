using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Diff", 4)]
    [NodeWidth(150)]
    public class Vector4Diff : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4 a;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4 b;

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
