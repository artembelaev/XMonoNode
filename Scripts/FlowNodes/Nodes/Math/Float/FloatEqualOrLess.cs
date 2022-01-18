using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/EqualOrLess", -175)]
    [NodeWidth(160)]
    public class FlotEqualOrLess : MonoNode
    {
        [Input] public float a;
        [Input] public float b;
        [Output] public bool result;

        private NodePort portA;
        private NodePort portB;
        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));

            GetOutputPort(nameof(result)).label = "A less or equal than B"; 
        }

        public override object GetValue(NodePort port)
        {
            return portA.GetInputValue(a) <= portB.GetInputValue(b);
        }
    }
}