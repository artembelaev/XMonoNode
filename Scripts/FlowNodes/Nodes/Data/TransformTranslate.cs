using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Translate", 468)]
    public class TransformTranslate : SetObjectParameter<Transform, Vector3> 
    {
        [SerializeField, NodeEnum]
        private Space relativeTo = Space.Self;

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Translation";
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.Translate(value, relativeTo);
        }
    }
}
