using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/GetPosition", 416)]
    public class GetObjectPosition : MonoNode
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public GameObject Target;

        [Output] public Vector3 _position;
        [Output] public Vector3 x;
        [Output] public Vector3 y;
        [Output] public Vector3 z;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            var target = GetInputValue(nameof(Target), Target);
            if (target == null)
            {
                return null;
            }

            if (port.fieldName == nameof(_position))
            {
                return target.transform.position;
            }

            if (port.fieldName == nameof(x))
            {
                return target.transform.position.x;
            }

            if (port.fieldName == nameof(y))
            {
                return target.transform.position.y;
            }

            if (port.fieldName == nameof(z))
            {
                return target.transform.position.z;
            }

            return null; // Replace this
        }
    }
}
