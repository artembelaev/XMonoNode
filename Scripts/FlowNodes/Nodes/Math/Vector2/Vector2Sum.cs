using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector2/Sum", 3)]
    [NodeWidth(135)]
    public class Vector2Sum : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2 a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector2 b;

        [Output] public Vector2 sum;

        NodePort inputPortA;
        NodePort inputPortB;

        protected override void Init()
        {
            base.Init();
            NodePort port = GetOutputPort(nameof(sum));
            if (port != null)
            {
                port.label = "A + B";
            }
            inputPortA = GetInputPort(nameof(a));
            inputPortB = GetInputPort(nameof(b));
        }

        public override object GetValue(NodePort port)
        {
            return inputPortA.GetInputValue(a) + inputPortB.GetInputValue(b);
        }
    }
}
