using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Events/Lifecicle/OnDestroy", 19)]
    //[ExecuteInEditMode]
    [NodeWidth(150)]
    public class OnDestroyed : EventNode
    {
        private void Reset()
        {
            Name = "On Destroy";
        }

        private void OnDestroy()
        {
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
