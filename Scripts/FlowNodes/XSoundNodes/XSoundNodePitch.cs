using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Изменяет свойство pitch
    /// </summary>
    [AddComponentMenu("X Sound Node/Pitch", 54)]
    [CreateNodeMenu("Sound/Pitch", 54)]
    [NodeWidth(160)]
    public class XSoundNodePitch : FlowNodeInOut
    {
        [Inline]
        [Input(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)]
        public AudioSources audioInput;

        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)]
        public AudioSources audioOutput;

        [Input(connectionType: ConnectionType.Override)]
        public float                    pitch = 1.0f;

        protected NodePort audioInputPort = null;
        protected NodePort audioOutputPort = null;
        protected NodePort pitchPort = null;

        protected override void Init()
        {
            base.Init();

            audioInputPort = GetInputPort(nameof(audioInput));
            audioOutputPort = GetOutputPort(nameof(audioOutput));
            pitchPort = GetInputPort(nameof(pitch));

            audioInputPort.label = "Input";
            audioOutputPort.label = "Output";
        }

        protected AudioSources GetAudioInput()
        {
            AudioSources sources = audioInputPort.GetInputValue(audioInput);
            if (sources == null)
            {
                sources = new AudioSources();
            }
            return sources;
        }

        private void Reset()
        {
            Name = "Pitch";
        }

        public override void Flow(NodePort flowPort)
        {
            changePitch();
            FlowOut();
        }

        public override object GetValue(NodePort port)
        {
            if (port == audioOutputPort)
            {
                return changePitch();
            }
            else
                return null;
        }

        private object changePitch()
        {
            pitch = pitchPort.GetInputValue(pitch);

            AudioSources sources = GetAudioInput();

            foreach (AudioSource source in sources.List)
            {
                if (source == null)
                    continue;
                source.pitch = pitch;
            }
            return sources;
        }
    }
}
