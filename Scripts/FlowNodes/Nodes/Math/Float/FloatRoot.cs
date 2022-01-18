using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Root", -192)]
    [NodeWidth(160)]
    public class FloatRoot : MonoNode
    {
        [Input] public float x;
        [Input] public float n = 2;
        [Output] public float result;

        private NodePort portA;
        private NodePort portB;
        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(x));
            portB = GetInputPort(nameof(n));

            GetOutputPort(nameof(result)).label = "X ^ (1/n)";
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.Pow(portA.GetInputValue(x), 1/portB.GetInputValue(n));
        }
    }
}