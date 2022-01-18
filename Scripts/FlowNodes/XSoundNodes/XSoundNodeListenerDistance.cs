using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ¬оспроизводит звук, расположенный не далее distance
    /// </summary>
    [AddComponentMenu("X Sound Node/Listener Distance", 190)]
    [CreateNodeMenu("Sound/Listener Distance", 190)]
    [NodeWidth(160)]
    public class XSoundNodeListenerDistance : XSoundNodeSimpleOutput
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public float                    maxDistance = 50f;

        private AudioListener   listener = null;

        private void Reset()
        {
            Name = "Listener distance";
        }
        public override object GetValue(NodePort port)
        {
            if (listener == null)
            {
                listener = FindObjectOfType<AudioListener>();
            }

            maxDistance = GetInputValue("maxDistance", maxDistance);

            AudioSources sources = GetAudioInput();
            AudioSources result = new AudioSources();
            foreach (AudioSource source in sources.List)
            {
                if (source == null)
                    continue;

                if (listener == null ||
                    Vector3.Distance(source.transform.position, listener.transform.position) <= maxDistance)
                {
                    result.List.Add(source);
                }
            }
            return result;
        }
    }
}
