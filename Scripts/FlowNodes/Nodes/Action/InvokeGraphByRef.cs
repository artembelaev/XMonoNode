using UnityEngine.Events;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [NodeWidth(180)]
    [CreateNodeMenu("Action/Invoke Graph By Ref", 15)]
    public class InvokeGraphByRef : FlowNodeInOut
    {
        [Output, Hiding]
        public Flow                         onEnd;

        [HideLabel]
        public FlowNodeGraph                graphPrefab = null;

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None, dynamicPortList: true), Hiding]
        public string[] parameters = new string[0];

        private NodePort                    OnEndPort = null;
        private FlowNodeGraphContainerItem  instantiator = null;


        public FlowNodeGraph Get()
        {
            if (graphPrefab.transform.IsChildOf(gameObject.transform))
            {
                return graphPrefab;
            }
            else
            {
                if (instantiator == null || instantiator.Prefab != graphPrefab as UnityEngine.Object)
                {
                    instantiator = new FlowNodeGraphContainerItem();
                }
                instantiator.SetGraphPrefab(graphPrefab);
                instantiator.Id = Name;
                return instantiator.Get(gameObject.transform);
            }
        }

        protected override void Init()
        {
            base.Init();

            FlowOutputPort.label = "On Start";

            OnEndPort = GetOutputPort(nameof(onEnd));
        }

        public override void Flow(NodePort flowPort)
        {
            object[] parameters = new object[this.parameters.Length];
            for (int i = 0; i < this.parameters.Length; ++i)
            {
                parameters[i] = GetPortFromList(nameof(parameters), i).GetInputValue();
            }

            FlowNodeGraph graph = Get();
            graph?.Flow(OnEnd, "flow", parameters);

            FlowOut();
        }

        private void OnEnd(string state)
        {
            FlowUtils.FlowOutput(OnEndPort);
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}
