using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Sum", 3)]
    [NodeWidth(150)]
    public class Vector3Sum : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3 a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3 b;

        [Output] public Vector3 sum;

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
