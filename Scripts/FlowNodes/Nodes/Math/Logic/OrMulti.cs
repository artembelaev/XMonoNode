using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/Or", 101)]
    [NodeWidth(110)]
    public class OrMulti : MonoNode
    {
        [Output]
        public bool or;

        [Input(
            backingValue: ShowBackingValue.Unconnected,
            connectionType: ConnectionType.Override,
            dynamicPortList: true)]

        public List<bool> inputs = new List<bool>();

        private void Reset()
        {
            inputs.Add(false);
            inputs.Add(false);
        }

        public override object GetValue(NodePort port)
        {
            return GetOr();
        }

        private bool GetOr()
        {
            if (inputs.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < inputs.Count; ++i)
            {
                NodePort port = GetPort(nameof(inputs) + " " + i);
                if (port != null)
                {
                    if (port.GetInputValue(inputs[i]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}