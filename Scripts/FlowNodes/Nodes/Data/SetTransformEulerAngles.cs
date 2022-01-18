using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Set Euler Angles", 456)]
    [NodeWidth(180)]
    public class SetTransformEulerAngles : SetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Set Euler Angles";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Euler Angles";
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.eulerAngles = value;
        }
    }
}
