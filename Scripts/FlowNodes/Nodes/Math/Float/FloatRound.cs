using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Round", -188)]
    [NodeWidth(160)]
    public class FloatRound : MonoNode
    {
        [Input] public float x;
        [Output] public float result;

        private NodePort portX;

        protected override void Init()
        {
            base.Init();
            portX = GetInputPort(nameof(x));

            GetOutputPort(nameof(result)).label = "Round(X)";
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.Round(portX.GetInputValue(x));
        }
    }
}