using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Events/Lifecicle/OnDisable", 18)]
    //[ExecuteInEditMode]
    [NodeWidth(150)]
    public class OnDisabled : EventNode
    {
        private void Reset()
        {
            Name = "On Disable";
        }

        private void OnDisable()
        {
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
