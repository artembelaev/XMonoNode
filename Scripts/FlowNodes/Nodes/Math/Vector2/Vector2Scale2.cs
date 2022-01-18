using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Scale2", 6)]
    [NodeWidth(130)]
    public class Vector2Scale2 : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2  vector2;

        [Input(connectionType: ConnectionType.Override)]
        public Vector2  scale = Vector2.one;

        [Output] public Vector2 scaled;

        private NodePort vector2Port;
        private NodePort scalePort;

        protected override void Init()
        {
            base.Init();

            vector2Port = GetInputPort(nameof(vector2));
            scalePort = GetInputPort(nameof(scale));
        }

        public override object GetValue(NodePort port)
        {
            vector2 = vector2Port.GetInputValue(vector2);
            Vector3 result = vector2;
            result.Scale(scalePort.GetInputValue(scale));
            return result;
        }
    }
}
