using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/SignedAngle", 12)]
    [NodeWidth(160)]
    public class Vector3SignedAngle : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector3  from;

        [Input(connectionType: ConnectionType.Override)]
        public Vector3  to;

        [Input(connectionType: ConnectionType.Override)]
        public Vector3  axis;

        [Output] public Vector3 angle;

        private NodePort fromPort;
        private NodePort toPort;
        private NodePort axisPort;

        protected override void Init()
        {
            base.Init();

            fromPort = GetInputPort(nameof(from));
            toPort = GetInputPort(nameof(to));
            axisPort = GetInputPort(nameof(axis));
        }

        public override object GetValue(NodePort port)
        {
            return Vector3.SignedAngle(
                fromPort.GetInputValue(from),
                toPort.GetInputValue(to),
                axisPort.GetInputValue(axis));
        }
    }
}
