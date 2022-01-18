using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Add", -199)]
    [NodeWidth(170)]
    public class AddFloat : MonoNode
    {
        public enum Operation
        {
            Add,
            Substract,
            Multiply,
            Divide,
        }

        [Input] public float FloatA;
        [Input] public float FloatB;
        public Operation operation;
        [Output] public float Result;

        protected NodePort FloatAPort = null;
        protected NodePort FloatBPort = null;
        protected NodePort ResultPort = null;

        protected override void Init()
        {
            base.Init();

            FloatAPort = GetInputPort(nameof(FloatA));
            FloatBPort = GetInputPort(nameof(FloatB));
            ResultPort = GetOutputPort(nameof(Result));

        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            var a = FloatAPort.GetInputValue(FloatA);
            var b = FloatBPort.GetInputValue(FloatB);
            switch (operation)
            {
                case Operation.Add:
                    return a + b;
                case Operation.Substract:
                    return a - b;
                case Operation.Multiply:
                    return a * b;
                default://case Operation.Divide:
                    return a / b;
            }

        }
    }
}