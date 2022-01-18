using System.Collections.Generic;
using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    public static class FlowUtils
    {
        public static void FlowOutput(NodePort output)
        {
            //if (output == null)
            //{
            //    return;
            //}

           // var connectedInputPorts = new List<NodePort>();
            
            for (int i = 0; i < output.ConnectionCount; ++i)
            {
                var inputPort = output.GetConnection(i);
                if (inputPort != null/* && inputPort.ValueType == typeof(Flow)*/)
                {
                   // connectedInputPorts.Add(inputPort);
                    FlowInput(inputPort);
                }
            }
        }

        public static void FlowInput(NodePort port)
        {
            var flowNode = port.node as IFlowNode;
            if (flowNode != null)
            {
                flowNode.Flow(port);
            }
        }

        public static void Flow(NodePort port)
        {
            if (port.direction == NodePort.IO.Output)
            {
                FlowOutput(port);
            }
            else
            {
                FlowInput(port);
            }
        }
    }
}