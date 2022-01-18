using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Set Local Position", 454)]
    [NodeWidth(180)]
    public class SetTransformLocalPosition : SetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Set Local Position";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Local Position";
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.localPosition = value;
        }
    }
}
