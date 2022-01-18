using UnityEngine;

namespace XMonoNode
{
    //[ExecuteInEditMode]
    [NodeWidth(110)]
    [CreateNodeMenu("Events/OnFlowStart", 0)]
    [AddComponentMenu("Mono Node/OnFlow", 0)]
    [NodeTint(40, 60, 105)]
    public class OnFlowEventNode : EventNode
    {
        protected override void Init()
        {
            base.Init();
            NodePort portIn = GetOutputPort(nameof(FlowOutput));
            if (portIn != null)
            {
                portIn.label = "Flow";
            }
        }

        private void Reset()
        {
            Name = "OnFlowStart";
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
