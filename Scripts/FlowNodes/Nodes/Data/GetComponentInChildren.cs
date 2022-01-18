using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    public abstract class GetComponentInChildren<ComponemtType> : GetComponentBase<ComponemtType> where ComponemtType : Component
    {
        [Input(connectionType: ConnectionType.Override), Hiding]
        public string objectName;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public bool includeInactive;

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

            objectName = GetInputValue(nameof(objectName), objectName);

            Component[] components = t.GetComponentsInChildren(typeof(ComponemtType), GetInputValue(nameof(includeInactive), includeInactive));
            foreach (Component comp in components)
            {
                if (comp.name.Equals(objectName) || string.IsNullOrWhiteSpace(objectName))
                    return comp;
            }
            return null;
        }
    }
}
