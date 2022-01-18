using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Events/Lifecicle/OnStart", 13)]
    //[ExecuteInEditMode]
    [NodeWidth(150)]
    public class OnStart : EventNode
    {
        private void Start()
        {
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
