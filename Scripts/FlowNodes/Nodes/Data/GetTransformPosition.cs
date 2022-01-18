using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Get Position", 451)]
    [NodeWidth(180)]
    public class GetTransformPosition : MonoNode
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public Transform _transform;
        [Output]
        public Vector3 _position;

        private NodePort transformPort;

        private void Reset()
        {
            Name = "Get Position";
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

            return target.position;
        }
    }
}
