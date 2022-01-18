using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace XMonoNode
{
    public abstract class SetObjectParameter<ObjType, ParamType> : FlowNodeInOut where ObjType : Object
    {
        [Input(connectionType: ConnectionType.Override)]
        public ObjType _object;

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public ParamType parameter;

        public NodePort ObjectPort => objectPort;
        private NodePort objectPort;

        public NodePort ParameterPort => parameterPort;
        private NodePort parameterPort;

        protected override void Init()
        {
            base.Init();

            objectPort = GetInputPort(nameof(_object));
            parameterPort = GetInputPort(nameof(parameter));

#if UNITY_EDITOR
            try
            {
                objectPort.label = UnityEditor.ObjectNames.NicifyVariableName(typeof(ObjType).PrettyName());
                parameterPort.label = UnityEditor.ObjectNames.NicifyVariableName(typeof(ParamType).PrettyName());
            }
            catch
            {
            }
#endif
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void Flow(NodePort flowPort)
        {
            object obj = ObjectPort.GetInputValue();
            if (obj is ObjType)
            {
                _object = obj as ObjType;
            }
            else if (obj is Component)
            {
                _object = (obj as Component).GetComponent<ObjType>();
            }

            if (_object != null)
            {
                SetValue(_object, parameterPort.GetInputValue(parameter));
            }
            FlowOut();
        }

        protected abstract void SetValue(ObjType obj, ParamType value);
    }
}
