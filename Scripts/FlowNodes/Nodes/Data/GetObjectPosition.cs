using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/GetPosition", 416)]
    [NodeWidth(160)]
    public class GetObjectPosition : MonoNode
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Inline, HideLabel]
        public Transform Target;

        [Output] public Vector3 _pos;

        [Output, Hiding]
        public float x;
        [Output, Hiding]
        public float y;
        [Output, Hiding]
        public float z;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            var target = GetInputValue(nameof(Target), Target);
            if (target == null)
            {
                return null;
            }

            if (port.fieldName == nameof(_pos))
            {
                return target.position;
            }

            if (port.fieldName == nameof(x))
            {
                return target.position.x;
            }

            if (port.fieldName == nameof(y))
            {
                return target.position.y;
            }

            if (port.fieldName == nameof(z))
            {
                return target.position.z;
            }

            return null; // Replace this
        }
    }
}
