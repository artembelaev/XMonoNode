using UnityEngine;
using XMonoNode;
using System.Collections.Generic;

namespace XMonoNode
{
    [NodeWidth(280)]
    public abstract class SwitchNodeBase<T> : FlowNodeInOut
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.InheritedAny)]
        public T    Switch = default(T);

        [Output(dynamicPortList: true, backingValue: ShowBackingValue.Always), NodeInspectorButton, FlowPort]
        public T[]  Case = new T[0];

        protected NodePort SwitchPort = null;

        private List<NodePort>  CasePorts = null;

        protected override void Init()
        {
            base.Init();
            NodePort portIn = GetInputPort(nameof(FlowInput));
            if (portIn != null)
            {
                portIn.label = "Switch";
            }
            NodePort portOut = GetOutputPort(nameof(FlowOutput));
            if (portOut != null)
            {
                portOut.label = "Default";
            }

            SwitchPort = GetInputPort(nameof(Switch));

            CasePorts = new List<NodePort>();
            CasePorts.Capacity = Case.Length;
            for (int i = 0; i < Case.Length; ++i)
            {
                CasePorts.Add(GetOutputPort($"{nameof(Case)} {i}"));
            }
        }

        public override void Flow(NodePort flowPort)
        {
            if (Case.Length <= 0)
            {
                FlowUtils.FlowOutput(FlowOutputPort);
                return;
            }

            Switch = SwitchPort.GetInputValue(Switch);
            bool caseDefault = true;
            if (Switch != null)
            {
                for (int i = 0; i < Case.Length; i++)
                {
                    if (Switch.Equals(Case[i]))
                    {

                        //#if UNITY_EDITOR
                        //                        FlowUtils.FlowOutput(GetOutputPort($"{nameof(Case)} {i}"));
                        //#else
                        FlowUtils.FlowOutput(CasePorts[i]);
                        //#endif
                        caseDefault = false;
                        // return; may be multiple choices!
                    }
                }
            }

            if (caseDefault)
            {
                FlowOut();
            }
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            for (int i = 0; i < Case.Length; ++i)
            {
                if (port.fieldName == $"{nameof(Case)} {i}")
                {
                    return Case[i];
                }
            }
            return null;
        }
    }
}
