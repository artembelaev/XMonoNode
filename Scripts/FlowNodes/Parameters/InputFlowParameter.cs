using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    public abstract class InputFlowParameter : MonoNode
    {
        /// <summary>
        /// Значение параметра
        /// </summary>
        public abstract object GetDefaultValue();
    }

    /// <summary>
    /// Возвращает параметр, переданный в метод FlowNodeGraph.Flow()
    /// </summary>
    [NodeTint(50, 70, 105)]
    [NodeWidth(200)]
    public abstract class InputFlowParameter<T> : InputFlowParameter
    {
        [SerializeField]
        public int index = -1;

        [Output(backingValue: ShowBackingValue.Always), HideLabel] public T   output;

        private long UpdateParametersTimes = -1;

        private object cachedValue;

        /// <summary>
        /// Значение, используемое по умолчанию
        /// </summary>
        public T DefaultValue
        {
            get => output;
            set => output = value;
        }

        protected override void Init()
        {
            base.Init();

            GetOutputPort(nameof(output)).label = "Default";
        }

        private void Reset()
        {
            Name = "Input Param: " + NodeUtilities.PrettyName(typeof(T));
        }

        public override object GetValue(NodePort port)
        {
            FlowNodeGraph flowGraph = graph as FlowNodeGraph;
            if (flowGraph != null)
            {
                return GetCachedParamValue(flowGraph);
            }

            return output;
        }

        private object GetCachedParamValue(FlowNodeGraph flowGraph)
        {
            if (UpdateParametersTimes == flowGraph.UpdateParametersTimes)
            {
                return cachedValue;
            }

            if (index > -1 && index < flowGraph.FlowParametersArray.Length)
            {
                cachedValue = flowGraph.FlowParametersArray[index];
                UpdateParametersTimes = flowGraph.UpdateParametersTimes;
                return cachedValue;
            }
            else if (flowGraph.OutputFlowParametersDict.TryGetValue(Name, out cachedValue))
            {
                UpdateParametersTimes = flowGraph.UpdateParametersTimes;
                return cachedValue;
            }
            else
            {
                UpdateParametersTimes = flowGraph.UpdateParametersTimes;
                cachedValue = flowGraph.FlowParametersArray.Get<T>(output);
                return cachedValue;
            }

        }

        public override object GetDefaultValue()
        {
            return output;
        }
    }
}
