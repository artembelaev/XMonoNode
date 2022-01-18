using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XMonoNode
{
    public abstract class SelectBase: MonoNode
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override)]
        public bool condition;

        protected NodePort conditionPort;

        protected override void Init()
        {
            base.Init();

            conditionPort = GetInputPort(nameof(condition));
        }

        public abstract System.Type Type
        {
            get;
        }
    }

    public abstract class SelectNode<T> : SelectBase
    {
        [Output] public T output;

        [Input(connectionType: ConnectionType.Override)]
        public T inputTrue;

        [Input(connectionType: ConnectionType.Override)]
        public T inputFalse;

        public override System.Type Type => typeof(T);
  

        private NodePort outputPort;
        private NodePort inputTruePort;
        private NodePort inputFalsePort;

        protected override void Init()
        {
            base.Init();

            outputPort = GetOutputPort(nameof(output));
            inputTruePort = GetInputPort(nameof(inputTrue));
            inputFalsePort = GetInputPort(nameof(inputFalse));

            inputTruePort.label = "True";
            inputFalsePort.label = "False";
        }

        public override object GetValue(NodePort port)
        {
            return
                conditionPort.GetInputValue(condition) ?
                inputTruePort.GetInputValue<object>(inputTrue) :
                inputFalsePort.GetInputValue<object>(inputFalse);
        }
    }
}
