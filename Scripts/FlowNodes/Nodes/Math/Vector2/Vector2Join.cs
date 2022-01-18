using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Join", 1)]
    [NodeWidth(150)]
    public class Vector2Join : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public float x;

        [Input(connectionType: ConnectionType.Override)]
        public float y;

        [Output] public Vector2 vector2;

        private NodePort xPort;
        private NodePort yPort;

        protected override void Init()
        {
            base.Init();
            xPort = GetInputPort(nameof(x));
            yPort = GetInputPort(nameof(y));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return new Vector2(xPort.GetInputValue(x), yPort.GetInputValue(y));
        }
    }
}
