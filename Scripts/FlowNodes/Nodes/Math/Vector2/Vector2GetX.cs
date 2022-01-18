using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/GetX", -9)]
    [NodeWidth(135)]
    public class Vector2GetX : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2  a;

        [Output] public float   x;

        NodePort inputPort;

        protected override void Init()
        {
            base.Init();
            inputPort = GetInputPort(nameof(a));
        }

        public override object GetValue(NodePort port)
        {
            return inputPort.GetInputValue(a).x;
        }
    }
}
