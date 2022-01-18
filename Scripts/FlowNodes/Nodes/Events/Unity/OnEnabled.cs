using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Events/Lifecicle/OnEnable", 11)]
    //[ExecuteInEditMode]
    [NodeWidth(150)]
    public class OnEnabled : EventNode
    {
        private void Reset()
        {
            Name = "On Enable";
        }

        private void OnEnable()
        {
            OnNodeEnable();
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
