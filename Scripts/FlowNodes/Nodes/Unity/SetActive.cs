using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/SetActive", 404)]
    [NodeWidth(170)]
    public class SetActive : FlowNodeInOut 
    {
        public enum ActiveOptions
        {
            Enable,
            Disable,
            Toggle,
        }

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public GameObject Target;

        public ActiveOptions Options;

        public override void Flow(NodePort flowPort)
        {
            var target = GetInputValue<GameObject>(nameof(Target), Target);
            var isActive = Options == ActiveOptions.Enable ? true : Options == ActiveOptions.Disable ? false : !target.activeSelf;
            target.SetActive(isActive);
            FlowOut();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
