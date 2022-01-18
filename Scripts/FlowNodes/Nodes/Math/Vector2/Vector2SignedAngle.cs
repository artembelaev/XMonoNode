using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/SignedAngle", 12)]
    [NodeWidth(135)]
    public class Vector2SignedAngle : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector2  from;

        [Input(connectionType: ConnectionType.Override)]
        public Vector2  to;

        [Output] public Vector2 angle;

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
            return Vector2.SignedAngle(
                fromPort.GetInputValue(from),
                toPort.GetInputValue(to));
        }
    }
}
