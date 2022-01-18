using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/IsActive", 403)]
    [NodeWidth(170)]
    public class IsActive : MonoNode
    {
        [Input] public GameObject Target;
        [Output] public bool Output;

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(Output))
            {
                GameObject target = GetInputValue(nameof(Target), Target);
                return target ? target.activeSelf : false;
            }
            return null; // Replace this
        }
    }
}
