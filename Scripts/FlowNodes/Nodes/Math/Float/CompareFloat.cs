using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Compare", -173)]
    public class CompareFloat : MonoNode
    {
        public enum Operation
        {
            LessThan,
            EqualOrLessThan,
            Equal,
            EqualOrGreaterThan,
            GreaterThan,
        }

        [Input(connectionType: ConnectionType.Override)] public float InputA;
        public Operation MyOperation;
        [Input(connectionType: ConnectionType.Override)] public float InputB;
        [Output] public bool Result;

        protected NodePort InputAPort = null;
        protected NodePort InputBPort = null;

        protected override void Init()
        {
            base.Init();

            InputAPort = GetInputPort(nameof(InputA));
            InputBPort = GetInputPort(nameof(InputB));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            var a = InputAPort.GetInputValue(InputA);
            var b = InputBPort.GetInputValue(InputB);
            switch (MyOperation)
            {
                case Operation.Equal:
                    return (a - b) * (a - b) <= 0.0001f;
                case Operation.EqualOrGreaterThan:
                    return a >= b;
                case Operation.EqualOrLessThan:
                    return a <= b;
                case Operation.GreaterThan:
                    return a > b;
                default://case Operation.LessThan:
                    return a < b;
            }
        }
    }
}
