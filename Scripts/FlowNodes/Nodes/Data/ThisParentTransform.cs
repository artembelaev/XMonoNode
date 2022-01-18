using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Transform/This Parent", 441)]
    [NodeWidth(140)]
    public class ThisParentTransform : MonoNode
    {
        [Output] public Transform parent;


        private void Reset()
        {
            Name = "This Parent";
        }

        public override object GetValue(NodePort port)
        {
            return gameObject.transform.parent;
        }
    }
}
