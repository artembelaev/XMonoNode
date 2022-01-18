using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("String/" + nameof(ConcatString))]
    [NodeWidth(180)]
    public class ConcatString : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public string First;
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public string Second;

        [Output] public string Result;

        private NodePort FirstPort;
        private NodePort SecondPort;

        protected override void Init()
        {
            base.Init();
            FirstPort = GetInputPort(nameof(First));
            SecondPort = GetInputPort(nameof(Second));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            var first = FirstPort.GetInputValue(First as object);
            var second = SecondPort.GetInputValue(Second as object);

            return $"{first}{second}";

        }
    }
}
