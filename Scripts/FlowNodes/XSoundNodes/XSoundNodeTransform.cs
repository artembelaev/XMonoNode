using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ¬оспроизводит звук, расположенный не далее distance
    /// </summary>
    [AddComponentMenu("X Sound Node/Transform Binding", 204)]
    [CreateNodeMenu("Sound/Transform Binding", 204)]
    [NodeWidth(180)]
    public class XSoundNodeTransform : XSoundNodeSimpleOutput
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), HideLabel]
        public Transform                    parentTransform = null;

        private void Reset()
        {
            Name = "Transform Binding";
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(audioOutput))
            {
                parentTransform = GetInputValue(nameof(parentTransform), parentTransform);

                AudioSources sources = GetAudioInput();
                foreach (AudioSource source in sources.List)
                {
                    if (source == null)
                        continue;

                    source.transform.SetParent(parentTransform);
                    source.transform.localPosition = Vector3.zero;
                }
                return sources;
            }

            return null;
        }
    }
}
