using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/MakeColor", 401)]
    [NodeWidth(150)]
    public class MakeColor : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public float r;

        [Input(connectionType: ConnectionType.Override)]
        public float g;

        [Input(connectionType: ConnectionType.Override)]
        public float b;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public float a  = 1.0f;

        [Output] public Color color;

        private NodePort rPort;
        private NodePort gPort;
        private NodePort bPort;
        private NodePort aPort;

        protected override void Init()
        {
            base.Init();
            rPort = GetInputPort(nameof(r));
            gPort = GetInputPort(nameof(g));
            bPort = GetInputPort(nameof(b));
            aPort = GetInputPort(nameof(a));
        }

        public override object GetValue(NodePort port)
        {
            return new Color(rPort.GetInputValue(r), gPort.GetInputValue(g), bPort.GetInputValue(b), aPort.GetInputValue(a));
        }
    }
}
