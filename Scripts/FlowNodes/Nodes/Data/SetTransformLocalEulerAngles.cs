using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Set Local Euler Angles", 458)]
    [NodeWidth(180)]

    public class SetTransformLocalEulerAngles : SetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Set Local Euler Angles";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Local Euler Angles";
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.eulerAngles = value;
        }
    }
}
