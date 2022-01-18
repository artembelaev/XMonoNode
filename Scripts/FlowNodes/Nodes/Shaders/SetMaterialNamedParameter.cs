using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    public abstract class SetMaterialNamedParameter<ParamType> : SetObjectParameter<Material, ParamType>
    {
        [Input(connectionType: ConnectionType.Override)]
        public string paramName;

        private NodePort namePort;

        public NodePort NamePort => namePort;

        protected override void Init()
        {
            base.Init();

            namePort = GetInputPort(nameof(paramName));
        }
    }
}
