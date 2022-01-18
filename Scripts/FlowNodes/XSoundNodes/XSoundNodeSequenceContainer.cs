using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Смешивает входящие звуки
    /// </summary>
    [AddComponentMenu("X Sound Node/Sequence Container", 302)]
    [CreateNodeMenu("Sound/Sequence Container", 302) ]
    [NodeWidth(150)]
    public class XSoundNodeSequenceContainer : XSoundNodesList
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Hiding]
        public float                    timeToReset = -1f;

        private int                     index = 0;
#if UNITY_EDITOR
        private System.DateTime         lastPlayTimeEditor = System.DateTime.Now;
#else
        private float                   lastPlayTime = 0f;
#endif

        private void Reset()
        {
            Name = "Sequence";
            inputs.Add(new AudioSources());
            inputs.Add(new AudioSources());
            inputs.Add(new AudioSources());
        }

        public bool timeLeft()
        {
            bool left = false;
#if UNITY_EDITOR
            System.DateTime currentTime = System.DateTime.Now;
            left = timeToReset > 0 && (currentTime - lastPlayTimeEditor).TotalSeconds > timeToReset;
            lastPlayTimeEditor = currentTime;
#else
            float currentTime = Time.time;
            left = timeToReset > 0 && currentTime - lastPlayTime > timeToReset;
            lastPlayTime = currentTime;
#endif
            return left;
        }

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(output))
            {
                var output = base.GetValue(port) as AudioSources;
                List<NodePort> inputPorts = GetAudioInputs();

                if (inputPorts.Count != 0)
                {
                    timeToReset = GetInputValue<float>("timeToReset", timeToReset);

                    if (timeLeft())
                    {
                        index = 0;
                    }

                    if (index >= inputPorts.Count)
                    {
                        index = 0;
                    }

                    output = inputPorts[index].GetInputValue<AudioSources>();
                    index++;
                }
                else
                {
                    Debug.LogError(gameObject.name + "(" + name + "): input ports count is null!", this);
                }

                return output;
            }
            else
            {
                return null;
            }
        }
    }
}
