using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Base node class for output flow parameters. Provides output of graph calculation results
    /// </summary>
    public abstract class OutputFlowParameter : MonoNode
    {
        public abstract object ValueAsObject
        {
            get;
        }
    }

    /// <summary>
    /// Provides using output parameters of different types
    /// </summary>
    [NodeTint(50, 70, 105)]
    [NodeWidth(200)]
    public abstract class OutputFlowParameter<T> : OutputFlowParameter
    {
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Unconnected)]
        public T    input;

        public T Value
        {
            get
            {
                return inputPort.GetInputValue(input);
            }
        }

        public override object ValueAsObject
        {
            get => Value;
        }

        public NodePort InputPort => inputPort;
        private NodePort inputPort = null;


        private void Reset()
        {
            Name = "Output: " + NodeUtilities.PrettyName(typeof(T));
        }

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(input));
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

    }
}
