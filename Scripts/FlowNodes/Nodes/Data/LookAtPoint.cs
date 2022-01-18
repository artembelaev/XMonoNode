using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/LookAt (worldPosition)", 466)]
    [NodeWidth(180)]
    public class LookAtPoint : SetObjectParameter<Transform, Vector3> 
    {
        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "World Position";
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.LookAt(value);
        }
    }
}
