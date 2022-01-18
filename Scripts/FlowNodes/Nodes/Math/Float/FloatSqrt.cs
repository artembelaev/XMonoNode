using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Sqrt", -190)]
    [NodeWidth(160)]
    public class FloatSqrt : MonoNode
    {
        [Input] public float x;
        [Output] public float result;

        private NodePort portX;

        protected override void Init()
        {
            base.Init();
            portX = GetInputPort(nameof(x));

            GetOutputPort(nameof(result)).label = "Sqrt(X)";
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.Sqrt(portX.GetInputValue(x));
        }
    }
}