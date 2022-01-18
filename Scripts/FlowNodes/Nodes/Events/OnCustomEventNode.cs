using UnityEngine;

namespace XMonoNode
{
   // [ExecuteInEditMode]
    [NodeWidth(170)]
    [CreateNodeMenu("Events/CustomEventStart", 1)]
    [AddComponentMenu("Mono Node/CustomEvent", 1)]
    [NodeTint(40, 60, 105)]
    public class OnCustomEventNode : EventNode
    {
        protected override void Init()
        {
            base.Init();

            FlowOutputPort.label = "CustomEvent Start";
        }

        private void Reset()
        {
            Name = "CustomEvent";
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
