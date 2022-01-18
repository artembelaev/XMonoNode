using UnityEngine;
using UnityEngine.UI;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("UI/GetButton", 413)]
    [NodeWidth(190)]
    public class GetButtonComponent : GetComponentBase<Button>
    {

        private void Reset()
        {
            Name = "Get Button";
        }
    }
}
