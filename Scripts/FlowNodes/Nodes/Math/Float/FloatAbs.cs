using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Abs", -189)]
    [NodeWidth(160)]
    public class FloatAbs : MonoNode
    {
        [Input] public float x;
        [Output] public float result;

        private NodePort portX;

        protected override void Init()
        {
            base.Init();
            portX = GetInputPort(nameof(x));

            GetOutputPort(nameof(result)).label = "|X|";
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.Abs(portX.GetInputValue(x));
        }
    }
}