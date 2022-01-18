using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Div", -195)]
    [NodeWidth(150)]
    public class FloatDiv : MonoNode
    {
        [Input] public float a;
        [Input] public float b = 100f;
        [Output] public float result;

        private NodePort portA;
        private NodePort portB;

        private void Reset()
        {
            Name = "/";
        }

        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));

            GetOutputPort(nameof(result)).label = "A / B";
        }

        public override object GetValue(NodePort port)
        {
            return portA.GetInputValue(a) / portB.GetInputValue(b);
        }
    }
}