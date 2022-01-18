using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Local Euler Angles", 457)]
    [NodeWidth(170)]
    public class GetTransformLocalEulerAngles : GetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Local Euler Angles";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Local Euler Angles";
        }

        protected override Vector3 GetValue(Transform obj)
        {
            return obj.localEulerAngles;
        }
    }
}
