using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    /// <summary>
    /// Изменяет свойство volume
    /// </summary>
    [AddComponentMenu("X Sound Node/Set Time", 203)]
    [CreateNodeMenu("Sound/Set Time", 203)]
    [NodeWidth(180)]
    public class XSoundNodeSetTime : FlowNodeInOut
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public AudioSources input;

        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)]
        public AudioSources output;

        [Input(connectionType: ConnectionType.Override)]
        public float time = 0f;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public bool normalized = false;

        private NodePort audioInputPort = null;
        private NodePort audioOutputPort = null;

        private NodePort timePort = null;
        private NodePort normalizedPort = null;

        protected override void Init()
        {
            base.Init();

            audioInputPort = GetInputPort(nameof(input));
            audioOutputPort = GetOutputPort(nameof(output));

            timePort = GetInputPort(nameof(time));
            normalizedPort = GetInputPort(nameof(normalized));

        }

        private AudioSources GetAudioInput()
        {
            AudioSources sources = audioInputPort.GetInputValue(input);
            if (sources == null)
            {
                sources = new AudioSources();
            }
            return sources;
        }

        private void Reset()
        {
            Name = "Set Time";
        }

        public override void Flow(NodePort flowPort)
        {
            changeTime();
            FlowOut();
        }

        public override object GetValue(NodePort port)
        {
            if (port == audioOutputPort)
            {
                return changeTime();
            }
            else
                return null;
        }

        private object changeTime()
        {
            AudioSources sources = GetAudioInput();
            normalized = normalizedPort.GetInputValue(normalized);
            time = timePort.GetInputValue(time);

            foreach (AudioSource source in sources.List)
            {
                if (source == null ||source.clip == null || source.clip.length == 0f)
                    continue;
                float _time = time;
                if (normalized)
                {
                    _time *= source.clip.length;
                }
                source.Stop();
                source.time = Mathf.Clamp(_time, 0f, source.clip.length - 0.001f);
                source.Play();
            }
            return sources;
        }
    }
}
