using XMonoNode;
using UnityEngine;
using System.Collections.Generic;

namespace XMonoNode
{
    [NodeWidth(110)]
    [CreateNodeMenu("Control/Join", 21)]
    public class FlowJoin : FlowNodeInOut
    {
        [Input(dynamicPortList: true), NodeInspectorButton]
        public Flow[]  waitAll = new Flow[2];


        private Dictionary<NodePort, bool> activePorts = new Dictionary<NodePort, bool>();

        private void Reset()
        {
            Name = "Join";
        }

        protected override void Init()
        {
            base.Init();

            FlowInputPort.label = "Reset";
        }

        public override void Flow(NodePort flowPort)
        {
            if (flowPort == FlowInputPort)
            {
                ResetInputPorts();
            }
            else // activate an input
            {
                activePorts[flowPort] = true;
                if (activePorts.Count == waitAll.Length) // all inputs activated
                {
                    FlowOut();
                    ResetInputPorts();
                }
            }
        }

        private void ResetInputPorts()
        {
            activePorts.Clear();
        }

        public override void Stop()
        {
            ResetInputPorts();
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
