using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Get Loca lPosition", 453)]
    [NodeWidth(180)]
    public class GetTransformLocalPosition : MonoNode
    {
        [Input(connectionType:ConnectionType.Override)]
        public Transform _transform;
        [Output]
        public Vector3 localPosition;

        private NodePort transformPort;

        private void Reset()
        {
            Name = "Get Local Position";
        }

        protected override void Init()
        {
            base.Init();

            transformPort = GetInputPort(nameof(_transform));
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            Transform target = transformPort.GetInputValue(_transform);
            if (target == null)
            {
                return Vector3.zero;
            }

            return target.localPosition;
        }
    }
}
