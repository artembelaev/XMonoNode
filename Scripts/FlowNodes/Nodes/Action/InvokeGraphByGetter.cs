using UnityEngine.Events;
using XMonoNode;

namespace XMonoNode
{
    [NodeWidth(250)]
    [CreateNodeMenu("Action/Invoke Graph By Getter", 14)]
    public class InvokeGraphByGetter : FlowNodeInOut
    {
        [Output, Hiding]
        public Flow                 onEnd;

        [HideLabel]
        public FlowNodeGraphGetter  getter = new FlowNodeGraphGetter(false);

        private NodePort            OnEndPort = null;

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.None, dynamicPortList: true), Hiding]
        public string[] parameters;

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
            getter.SafeFlow(OnEnd, "flow", parameters);
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
