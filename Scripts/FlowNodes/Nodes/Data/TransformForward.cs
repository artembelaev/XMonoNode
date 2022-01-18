using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Forward", 461)]
    public class TransformForward : GetObjectParameter<Transform, Vector3>
    {
        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Forward";
        }

        protected override Vector3 GetValue(Transform obj)
        {
            return obj.forward;
        }
    }
}
