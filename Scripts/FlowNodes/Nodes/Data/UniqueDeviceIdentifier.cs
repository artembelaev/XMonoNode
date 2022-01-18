using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Utils/"+nameof(UniqueDeviceIdentifier))]
    [NodeWidth(190)]
    public class UniqueDeviceIdentifier : MonoNode
    {
        [Output] public string deviceId;

        public override object GetValue(NodePort port)
        {
            return port.fieldName == nameof(deviceId) ? SystemInfo.deviceUniqueIdentifier : null;
        }
    }
}