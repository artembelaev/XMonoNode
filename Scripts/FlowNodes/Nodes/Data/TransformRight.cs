using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Right", 462)]
    public class TransformRight : GetObjectParameter<Transform, Vector3>
    {
        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Right";
        }

        protected override Vector3 GetValue(Transform obj)
        {
            return obj.right;
        }
    }
}
