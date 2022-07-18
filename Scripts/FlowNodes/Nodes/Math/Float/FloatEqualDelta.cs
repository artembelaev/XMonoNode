using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Equal (delta)", -176)]
    [NodeWidth(150)]
    public class FloatEqualDelta : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel, Inline]
        public float a;
        [Output, HideLabel] public bool result;
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public float b;
        [Input(connectionType: ConnectionType.Override), Hiding]
        public float delta = 0.01f;
        

        private NodePort portA;
        private NodePort portB;
        private NodePort portDelta;
        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));
            portDelta = GetInputPort(nameof(delta));

            GetOutputPort(nameof(result)).label = "A == B";
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.Abs(portA.GetInputValue(a) - portB.GetInputValue(b)) < portDelta.GetInputValue(delta);
        }
    }
}