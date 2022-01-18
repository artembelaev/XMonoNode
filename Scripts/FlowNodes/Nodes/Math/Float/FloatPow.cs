using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Pow", -193)]
    [NodeWidth(160)]
    public class FloatPow : MonoNode
    {
        [Input] public float x;
        [Input] public float n;
        [Output] public float result;

        private NodePort portA;
        private NodePort portB;
        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(x));
            portB = GetInputPort(nameof(n));

            GetOutputPort(nameof(result)).label = "X ^ N";
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.Pow(portA.GetInputValue(x), portB.GetInputValue(n));
        }
    }
}