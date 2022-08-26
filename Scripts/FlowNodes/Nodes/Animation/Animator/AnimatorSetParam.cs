using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    public abstract class AnimatorSetParam : FlowNodeInOut 
    {
        [SerializeField, Hiding, HideLabel]
        public Animator animator;

        [SerializeField, Hiding, HideLabel]
        public string paramName;


        public override object GetValue(NodePort port) 
        {
            return null;
        }
    }

    public abstract class AnimatorSetParam<T> : AnimatorSetParam
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), HideLabel]
        public T                                Value;

        private NodePort                        ValuePort = null;

        protected override void Init()
        {
            base.Init();
            ValuePort = GetInputPort(nameof(Value));
        }

        public override void Flow(NodePort flowPort)
        {
            T value = ValuePort.GetInputValue(Value);
            SetAnimatorValue(animator, paramName, value);
            FlowOut();
        }

        protected abstract void SetAnimatorValue(Animator animator, string name, T value);
    }
}
