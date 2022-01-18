using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Sqr", -191)]
    [NodeWidth(160)]
    public class FloatSqr : MonoNode
    {
        [Input] public float x;
        [Output] public float result;

        private NodePort portX;

        protected override void Init()
        {
            base.Init();
            portX = GetInputPort(nameof(x));

            GetOutputPort(nameof(result)).label = "X ^ 2";
        }

        public override object GetValue(NodePort port)
        {
            float x = portX.GetInputValue(this.x);
            return x * x;
        }
    }
}