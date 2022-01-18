using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Normalized", 8)]
    [NodeWidth(135)]
    public class Vector2Normalized : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2  vector2;

        [Output] public Vector2 normalized;

        private NodePort inputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(vector2));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(vector2).normalized;
        }
    }
}
