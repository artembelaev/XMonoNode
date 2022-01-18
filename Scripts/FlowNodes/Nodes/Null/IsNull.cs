using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    [NodeWidth(110)]
    [CreateNodeMenu("Null/IsNull", 1012)]
    public class IsNull : MonoNode
    {
        [Inline]
        [Input(backingValue: ShowBackingValue.Never, connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None)]
        public Object value = null;

        [Output]
        public bool isNull;

        NodePort valuePort;

        protected override void Init()
        {
            base.Init();
            valuePort = GetInputPort(nameof(value));
        }

        public override object GetValue(NodePort port)
        {
            return valuePort.GetInputValue<object>() == null;
        }
    }
}
