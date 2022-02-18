using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Mult", -196)]
    [NodeWidth(110)]
    public class FloatMult : MonoNode
    {
        [Output] public float result;
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float a;
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float b;
        

        private NodePort portA;
        private NodePort portB;
        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));

            GetOutputPort(nameof(result)).label = "A x B";
        }

        public override object GetValue(NodePort port)
        {
            return portA.GetInputValue(a) * portB.GetInputValue(b);
        }
    }
}