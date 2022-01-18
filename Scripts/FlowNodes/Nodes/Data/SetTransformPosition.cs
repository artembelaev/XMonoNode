using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Set Position", 452)]
    [NodeWidth(180)]
    public class SetTransformPosition : SetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Set Position";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Position";
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.position = value;
        }
    }
}
