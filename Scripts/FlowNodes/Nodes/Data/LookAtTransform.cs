using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Look At (Transform)", 464)]
    [NodeWidth(170)]
    public class LookAtTransform : SetObjectParameter<Transform, Transform> 
    {
        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Target";
        }

        protected override void SetValue(Transform obj, Transform value)
        {
            obj.LookAt(value);
        }
    }
}
