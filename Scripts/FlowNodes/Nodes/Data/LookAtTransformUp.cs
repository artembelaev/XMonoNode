using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/LookAt (Transform, Up)", 465)]
    [NodeWidth(180)]
    public class LookAtTransformUp : SetObjectParameter<Transform, Transform> 
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Hiding]
        public Vector3 worldUp = Vector3.up;

        private NodePort worldUpPort;

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "Target";

            worldUpPort = GetInputPort(nameof(worldUp));
        }

        protected override void SetValue(Transform obj, Transform value)
        {
            obj.LookAt(value, worldUpPort.GetInputValue(worldUp));
        }
    }
}
