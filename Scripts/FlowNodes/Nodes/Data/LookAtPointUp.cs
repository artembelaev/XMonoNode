using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/LookAt (worldPosition, Up)", 467)]
    [NodeWidth(180)]
    public class LookAtPointUp : SetObjectParameter<Transform, Vector3> 
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Hiding]
        public Vector3 worldUp = Vector3.up;

        private NodePort worldUpPort;

        protected override void Init()
        {
            base.Init();
            ParameterPort.label = "World Position";

            worldUpPort = GetInputPort(nameof(worldUp));
        }

        protected override void SetValue(Transform obj, Vector3 value)
        {
            obj.LookAt(value, worldUpPort.GetInputValue(worldUp));
        }
    }
}
