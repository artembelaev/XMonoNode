using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/GetLocalPosition", 415)]
    [NodeWidth(170)]
    public class GetLocalPosition : MonoNode
    {
        [Input] public GameObject Target;
        [Output] public Vector3 localPosition;
        [Output, Hiding] public Vector3 x;
        [Output, Hiding] public Vector3 y;
        [Output, Hiding] public Vector3 z;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            var target = GetInputValue(nameof(Target), Target);
            if (target == null)
            {
                return null;
            }

            if (port.fieldName == nameof(localPosition))
            {
                return target.transform.localPosition;
            }

            if (port.fieldName == nameof(x))
            {
                return target.transform.localPosition.x;
            }

            if (port.fieldName == nameof(y))
            {
                return target.transform.localPosition.y;
            }

            if (port.fieldName == nameof(z))
            {
                return target.transform.localPosition.z;
            }

            return null; // Replace this
        }
    }
}
