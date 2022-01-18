using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/Get Component", 501)]
    [NodeWidth(230)]
    public class GetComponent : GetComponentBase<Component>
    {
        [Input(connectionType: ConnectionType.Override)]
        public string typeName;

        private void Reset()
        {
            Name = "Get Component";
        }

        protected override void Init()
        {
            base.Init();

            objPort.label = "Transform";
        }

        public override object GetValue(NodePort port)
        {
            Component t = objPort.GetInputValue() as Component;
            if (t == null)
            {
                t = obj;
            }

            if (t == null)
            {
                return null;
            }

            typeName = GetInputValue(nameof(typeName), typeName);

            return t.GetComponent(typeName);
        }
    }
}
