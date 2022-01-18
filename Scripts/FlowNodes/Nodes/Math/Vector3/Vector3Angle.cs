using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Angle", 11)]
    [NodeWidth(160)]
    public class Vector3Angle : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector3  from;

        [Input(connectionType: ConnectionType.Override)]
        public Vector3  to;

        [Output] public Vector3 angle;

        private NodePort fromPort;
        private NodePort toPort;

        protected override void Init()
        {
            base.Init();

            fromPort = GetInputPort(nameof(from));
            toPort = GetInputPort(nameof(to));
        }

        public override object GetValue(NodePort port)
        {
            return Vector3.Angle(fromPort.GetInputValue(from), toPort.GetInputValue(to));
        }
    }
}
