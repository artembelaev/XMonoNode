using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector4/Sum", 3)]
    [NodeWidth(150)]
    public class Vector4Sum : MonoNode
    {
        [Input] public Vector4 a;
        [Input] public Vector4 b;

        [Output] public Vector4 sum;

        NodePort inputPortA;
        NodePort inputPortB;

        protected override void Init()
        {
            base.Init();
            NodePort port = GetOutputPort(nameof(sum));
            if (port != null)
            {
                port.label = "A + B";
            }
            inputPortA = GetInputPort(nameof(a));
            inputPortB = GetInputPort(nameof(b));
        }

        public override object GetValue(NodePort port)
        {
            return inputPortA.GetInputValue(a) + inputPortB.GetInputValue(b);
        }
    }
}
