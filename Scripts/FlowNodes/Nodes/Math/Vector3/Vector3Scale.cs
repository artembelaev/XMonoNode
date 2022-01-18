using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Vector3/Scale", 5)]
    [NodeWidth(150)]
    public class Vector3Scale : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3  vector3;

        [Input(connectionType: ConnectionType.Override)]
        public float    scale;

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
            return vector3Port.GetInputValue(vector3) * scalePort.GetInputValue(scale);
        }
    }
}
