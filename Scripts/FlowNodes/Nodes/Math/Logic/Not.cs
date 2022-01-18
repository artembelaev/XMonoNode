using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/Not", 102)]
    [NodeWidth(110)]
    public class Not : MonoNode
    {
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never, typeConstraint: TypeConstraint.Inherited), Inline]
        public bool Input;

        [Output]
        public bool Result;

        protected NodePort InputPort = null;

        protected override void Init()
        {
            base.Init();

            InputPort = GetInputPort(nameof(Input));
        }

        public override object GetValue(NodePort port)
        {
            return !InputPort.GetInputValue(Input);
        }
    }
}