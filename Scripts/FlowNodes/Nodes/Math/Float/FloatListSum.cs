using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/ListSum", -156)]
    [NodeWidth(130)]
    public class FloatListSum : MonoNode
    {
        [Input(
            backingValue: ShowBackingValue.Unconnected,
            connectionType: ConnectionType.Override,
            dynamicPortList: true)]

        public List<float> inputs = new List<float>();

        [Output]
        public float sum;

        private void Reset()
        {
            inputs.Add(1);
            inputs.Add(2);
            inputs.Add(3);
        }

        public override object GetValue(NodePort port)
        {
            return GetSum();
        }

        private float GetSum()
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

            return sum;
        }
    }
}