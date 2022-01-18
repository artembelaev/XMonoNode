using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Euler Angles", 455)]
    [NodeWidth(170)]
    public class GetTransformEulerAngles : GetObjectParameter<Transform, Vector3>
    {
        private void Reset()
        {
            Name = "Euler Angles";
        }

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Euler Angles";
        }

        protected override Vector3 GetValue(Transform obj)
        {
            return obj.eulerAngles;
        }
    }
}
