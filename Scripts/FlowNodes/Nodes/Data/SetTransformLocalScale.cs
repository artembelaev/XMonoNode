using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Set Local Scale", 460)]
    [NodeWidth(180)]
    public class SetTransformLocalScale : SetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Set Local Scale";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Local Scale";
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.localScale = value;
        }
    }
}
