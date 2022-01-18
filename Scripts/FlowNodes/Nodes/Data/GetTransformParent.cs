using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Parent", 442)]
    [NodeWidth(170)]
    public class GetTransformParent : GetObjectParameter<Transform, Transform>
    {
        private void Reset()
        {
            Name = "Parent";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Parent";
        }

        protected override Transform GetValue(Transform obj)
        {
            return obj.parent;
        }
    }
}
