using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Set Parent", 443)]
    [NodeWidth(170)]
    public class SetTransformParent : SetObjectParameter<Transform, Transform>
    {
        private void Reset()
        {
            Name = "Parent";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Set Parent";
        }


        protected override void SetValue(Transform obj, Transform value)
        {
            obj.SetParent(value);
        }
    }
}
