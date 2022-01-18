using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Float/Min", -159)]
    [NodeWidth(130)]
    public class FloatMin : MonoNode
    {
        [Input(
            backingValue: ShowBackingValue.Unconnected,
            connectionType: ConnectionType.Override,
            dynamicPortList: true)]

        public List<float> inputs = new List<float>();

        [Output]
        public float min;

        private void Reset()
        {
            inputs.Add(1);
            inputs.Add(2);
            inputs.Add(3);
        }

        public override object GetValue(NodePort port)
        {
            return GetMin();
        }

        private float GetMin()
        {
            if (inputs.Count == 0)
            {
                return 0;
            }

            float min = inputs[0];

            for (int i = 0; i < inputs.Count; ++i)
            {
                NodePort port = GetPort(nameof(inputs) + " " + i);
                if (port != null)
                {
                    inputs[i] = port.GetInputValue(inputs[i]);
                }
                if (inputs[i] < min)
                {
                    min = inputs[i];
                }
            }

            return min;
        }
    }
}