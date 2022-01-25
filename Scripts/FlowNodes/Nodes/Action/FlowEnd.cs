using UnityEngine.Events;
using XMonoNode;
using UnityEngine;

namespace XMonoNode
{

    [CreateNodeMenu("Action/FlowEnd", 13)]
    [NodeTint(40, 60, 105)]
    [NodeWidth(130)]
    public class FlowEnd : FlowNodeIn
    {
        public System.Action<string> Action
        {
            get;
            set;
        }

        private void Reset()
        {
            Name = "FlowEnd";
        }

        public override void Flow(NodePort flowPort)
        {
            FlowNodeGraph flowGraph = graph as FlowNodeGraph;
            if (Action != null)
            {
                Action.Invoke(flowGraph != null ? flowGraph.State : "");
            }
            if (flowGraph != null && flowGraph.Container != null)
            {

                flowGraph.Container.PutIntoPool(flowGraph);
            }
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
