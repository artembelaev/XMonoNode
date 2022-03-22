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
#if UNITY_EDITOR
            OnNodeEnable();
#endif
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
