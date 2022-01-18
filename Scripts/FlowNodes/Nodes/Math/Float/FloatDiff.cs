using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Diff", -197)]
    [NodeWidth(160)]
    public class FloatDiff : MonoNode
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

            GetOutputPort(nameof(result)).label = "A - B";
        }

        public override object GetValue(NodePort port)
        {
            return portA.GetInputValue(a) - portB.GetInputValue(b);
        }
    }
}