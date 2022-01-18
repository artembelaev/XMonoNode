using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Local Scale", 459)]
    [NodeWidth(170)]
    public class GetTransformLocalScale : GetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Local Scale";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Local Scale";
        }

        protected override Vector3 GetValue(Transform obj)
        {
            return obj.localScale;
        }
    }
}
