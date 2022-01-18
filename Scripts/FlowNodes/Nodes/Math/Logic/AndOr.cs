using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/AndOr", 101)]
    [NodeWidth(180)]
    public class AndOr : MonoNode
    {
        public enum Operation
        {
            And,
            Or,
        }

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Inline]
        public bool InputA;

        [Output] public bool Result;

        [Input(connectionType: ConnectionType.Override)]
        public bool InputB;

        public Operation MyOperation;

        protected NodePort InputAPort = null;
        protected NodePort InputBPort = null;

        protected override void Init()
        {
            base.Init();

            InputAPort = GetInputPort(nameof(InputA));
            InputBPort = GetInputPort(nameof(InputB));
        }

        public override object GetValue(NodePort port)
        {
            var a = InputAPort.GetInputValue<bool>(InputA);
            var b = InputBPort.GetInputValue<bool>(InputB);
            return MyOperation == Operation.And ? a && b : a || b;
        }
    }
}
