using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Воозвращает длину звука
    /// </summary>
    [AddComponentMenu("X Sound Node/Get Length", 201)]
    [CreateNodeMenu("Sound/Get Length", 201)]
    [NodeWidth(140)]
    public class XSoundNodeGetLength : XSoundNodeSimple
    {
        [Output]
        public float                    length = 0.0f;

        private void Reset()
        {
            Name = "Get Length";
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(length))
            {
                length = 0.0f;

                AudioSources sources = GetAudioInput();
                if (sources.List.Count != 0)
                {
                    AudioSource source = sources.List[sources.List.Count-1];
                    if (source.isPlaying &&
                        source.clip != null &&
                        Mathf.Approximately(source.clip.length, 0.0f) == false)
                    {
                        length = source.clip.length;
                    }
                }
                return length;
            }

            return null;
        }
    }
}
