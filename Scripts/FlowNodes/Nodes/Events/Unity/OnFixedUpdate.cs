using UnityEngine;

namespace XMonoNode
{
    //[ExecuteInEditMode]
    [CreateNodeMenu("Events/Lifecicle/" + nameof(OnFixedUpdate), 16)]
    [NodeWidth(150)]
    public class OnFixedUpdate : EventNode
    {
        private void FixedUpdate()
        {
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
