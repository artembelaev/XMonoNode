using UnityEngine;
using TMPro;

namespace XMonoNode
{
    [CreateNodeMenu("UI/MakeVertexGradient", 402)]
    [NodeWidth(170)]
    public class MakeVertexGradient : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Color color0;

        [Input(connectionType: ConnectionType.Override)]
        public Color color1;

        [Input(connectionType: ConnectionType.Override)]
        public Color color2;

        [Input(connectionType: ConnectionType.Override)]
        public Color color3;

        [Output] public VertexGradient gradient;

        private NodePort color0Port;
        private NodePort color1Port;
        private NodePort color2Port;
        private NodePort color3Port;

        protected override void Init()
        {
            base.Init();
            color0Port = GetInputPort(nameof(color0));
            color1Port = GetInputPort(nameof(color1));
            color2Port = GetInputPort(nameof(color2));
            color3Port = GetInputPort(nameof(color3));
        }

        public override object GetValue(NodePort port)
        {
            return new VertexGradient(
                color0Port.GetInputValue(color0),
                color1Port.GetInputValue(color1),
                color2Port.GetInputValue(color2),
                color3Port.GetInputValue(color3));
        }
    }
}
