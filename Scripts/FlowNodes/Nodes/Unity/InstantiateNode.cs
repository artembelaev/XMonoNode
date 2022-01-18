using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("GameObject/Instantiate", 401)]
    [NodeWidth(170)]
    public class InstantiateNode : FlowNodeInOut
    {
        [Input(connectionType: ConnectionType.Override)]
        public GameObject Prefab;

        [Output] public GameObject Instance;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public Transform Parent;

        public override void Flow(NodePort flowPort)
        {
            var prefab = GetInputValue(nameof(Prefab), Prefab);
            var parent = GetInputValue(nameof(Parent), Parent);
            if (parent != null)
            {
                Instance = Instantiate(prefab, parent);
            }
            else
            {
                Instance = Instantiate(prefab);
            }
            FlowOut();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(Instance))
            {
                return Instance;
            }
            return null; // Replace this
        }
    }
}
