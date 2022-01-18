using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XMonoNode
{
    public abstract class VariableNode : FlowNodeInOut
    {
        public abstract System.Type Type
        {
            get;
        }

        /// <summary>
        /// If true, value of variable had been assigned by Flow()
        /// </summary>
        public bool Assigned
        {
            get;
            protected set;
        }
    }

    [NodeWidth(200)]
    public abstract class VariableNode<T> : VariableNode
    {
        [Input, Inline, HideLabel]
        public T inputValue = default(T);

        [Output]
        public T output = default(T);

        public T Value
        {
            get
            {
                if (!Assigned)
                {
                    AssignValue();
                }
                return inputValue;
            }
        }

        private NodePort inputValuePort;

        protected override void Init()
        {
            base.Init();

            inputValuePort = GetInputPort(nameof(inputValue));
        }

        private void AssignValue()
        {
            Assigned = true;
            inputValue = inputValuePort.GetInputValue(inputValue);
        }

        public override System.Type Type => typeof(T);

        private void Reset()
        {
            Name = "Variable: " + typeof(T).Name;
        }

        public override object GetValue(NodePort port)
        {
            return Value;
        }

        public override void Flow(NodePort flowPort)
        {
            AssignValue();
            FlowOut();
        }

    }
}



