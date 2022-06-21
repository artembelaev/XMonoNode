using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/And", 102)]
    [NodeWidth(110)]
    public class AndMulti : MonoNode
    {
        [Output]
        public bool And;

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
            return GetAnd();
        }

        private bool GetAnd()
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
                    if (!port.GetInputValue(inputs[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}