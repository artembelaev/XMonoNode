using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Scale3", 6)]
    [NodeWidth(150)]
    public class Vector3Scale3 : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  vector3;
        
        [Input(connectionType: ConnectionType.Override)]
        public Vector3  scale = Vector3.one;

        [Output] public Vector3 scaled;

        private NodePort vector3Port;
        private NodePort scalePort;

        protected override void Init()
        {
            base.Init();

            vector3Port = GetInputPort(nameof(vector3));
            scalePort = GetInputPort(nameof(scale));
        }

        public override object GetValue(NodePort port)
        {
            vector3 = vector3Port.GetInputValue(vector3);
            Vector3 result = vector3;
            result.Scale(scalePort.GetInputValue(scale));
            return result;
        }
    }
}
