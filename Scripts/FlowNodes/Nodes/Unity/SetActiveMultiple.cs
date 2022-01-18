using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/SetActiveMultiple", 405)]
    [NodeWidth(170)]
    public class SetActiveMultiple : FlowNodeInOut 
    {
        public enum ActiveOptions
        {
            Enable,
            Disable,
            Toggle,
        }

        [Input] public GameObject[] Target;
        public ActiveOptions Options;

        public override void Flow(NodePort flowPort)
        {
            var targets = GetInputValue(nameof(Target), Target);
            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                var isActive = Options == ActiveOptions.Enable ? true : Options == ActiveOptions.Disable ? false : !target.activeSelf;
                target.SetActive(isActive);
            }
            FlowOut();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
