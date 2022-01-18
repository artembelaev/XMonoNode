using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/TransformToRectTransform", 408)]
    [NodeWidth(190)]
    public class GetRectTransform : MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public Transform _transform;
        [Output] public RectTransform rectTransform;

        private NodePort transformPort;

        protected override void Init()
        {
            base.Init();

            transformPort = GetInputPort(nameof(_transform));
        }

        private void Reset()
        {
            Name = "Transform To RectTransform";
        }

        public override object GetValue(NodePort port)
        {
            RectTransform t = transformPort.GetInputValue(_transform) as RectTransform;
            if (t == null)
            {
                return null;
            }

            return t;
            
        }
    }
}
