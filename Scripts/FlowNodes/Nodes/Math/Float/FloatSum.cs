using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Sum", -198)]
    [NodeWidth(150)]
    public class FloatSum : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float a;
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float b;
        [Output] public float result;

        private NodePort portA;
        private NodePort portB;
        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));

            GetOutputPort(nameof(result)).label = "A + B";
        }

        public override object GetValue(NodePort port)
        {
            return portA.GetInputValue(a) + portB.GetInputValue(b);
        }
    }
}