using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Up", 463)]
    public class TransformUp : GetObjectParameter<Transform, Vector3>
    {
        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Up";
        }

        protected override Vector3 GetValue(Transform obj)
        {
            return obj.up;
        }
    }
}
