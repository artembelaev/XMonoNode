using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Avg", -157)]
    [NodeWidth(130)]
    public class FloatAvg : MonoNode
    {
        [Input(
            backingValue: ShowBackingValue.Always,
            connectionType: ConnectionType.Override,
            dynamicPortList: true)]
        public List<float> inputs = new List<float>();

        [Output]
        public float avg;

        private void Reset()
        {
            inputs.Add(1);
            inputs.Add(2);
            inputs.Add(3);
        }

        public override object GetValue(NodePort port)
        {
            return GetAvg();
        }

        private float GetAvg()
        {
            if (inputs.Count == 0)
            {
                return 0;
            }

            float sum = 0.0f;

            for (int i = 0; i < inputs.Count; ++i)
            {
                NodePort port = GetPort(nameof(inputs) + " " + i);
                if (port != null)
                {
                    inputs[i] = port.GetInputValue(inputs[i]);
                    sum += inputs[i];
                }
            }

            return sum / inputs.Count;
        }
    }
}