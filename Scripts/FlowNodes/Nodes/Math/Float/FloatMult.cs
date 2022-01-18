using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Mult", -196)]
    [NodeWidth(160)]
    public class FloatMult : MonoNode
    {
        [Input] public float a;
        [Input] public float b;
        [Output] public float result;

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