using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Animation/Play Animation", 1)]
    public class PlayAnimation : FlowNodeInOut 
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public Animator Target;

        [Input(connectionType: ConnectionType.Override)]
        public string StateName;

        private void Reset()
        {
            Name = "Play Animation";
        }

        public override void Flow(NodePort flowPort) 
        {
            var animator = GetInputValue(nameof(Target), Target);
            var stateName = GetInputValue(nameof(StateName), StateName);
            animator.Play(stateName);
            FlowOut();
        }

        public override object GetValue(NodePort port) 
        {
            return null;
        }
    }
}
