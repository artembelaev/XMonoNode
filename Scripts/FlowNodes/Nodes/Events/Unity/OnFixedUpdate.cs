using UnityEngine;

namespace XMonoNode
{
    //[ExecuteInEditMode]
    [CreateNodeMenu("Events/Lifecicle/" + nameof(OnFixedUpdate), 16)]
    [NodeWidth(150)]
    public class OnFixedUpdate : EventNode
    {
        public override void CustomFixedUpdate()
        {
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
