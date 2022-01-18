using XMonoNode;
using UnityEngine;

namespace XMonoNode
{
    [NodeWidth(80)]
    [CreateNodeMenu("Null/Null", 1010)]
    public class Null : MonoNode
    {
        [Output]
        public bool _Null;

        public override object GetValue(NodePort port)
        {
            return  null;
        }
    }
}
