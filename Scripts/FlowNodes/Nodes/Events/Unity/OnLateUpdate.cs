using UnityEngine;

namespace XMonoNode
{
    //[ExecuteInEditMode]
    [CreateNodeMenu("Events/Lifecicle/" + nameof(OnLateUpdate), 17)]
    [NodeWidth(150)]
    public class OnLateUpdate : EventNode
    {
        private void LateUpdate()
        {
            TriggerFlow();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
