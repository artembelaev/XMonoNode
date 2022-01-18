using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Базовый класс для нодов, принимающих один источник звука
    /// </summary>
    public abstract class XSoundNodeSimple : XSoundNodeBase
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public AudioSources audioInput;

        protected NodePort audioInputPort = null;

        protected override void Init()
        {
            base.Init();

            audioInputPort = GetInputPort(nameof(audioInput));

            audioInputPort.label = "Input";
        }

        protected AudioSources GetAudioInput()
        {
            AudioSources sources = audioInputPort.GetInputValue<AudioSources>();
            if (sources == null)
            {
                sources = new AudioSources();
            }
            return sources;
        }
    }

    public abstract class XSoundNodeSimpleOutput : XSoundNodeBase
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public AudioSources audioInput;
        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)]
        public AudioSources audioOutput;

        private NodePort inputPort;
        private NodePort outputPort;

        protected NodePort InputPort => inputPort;
        protected NodePort OutputPort => outputPort;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(audioInput));
            inputPort.label = "Input";

            outputPort = GetOutputPort(nameof(audioOutput));
            outputPort.label = "Output";
        }

        protected AudioSources GetAudioInput()
        {
            AudioSources sources = inputPort.GetInputValue<AudioSources>();
            if (sources == null)
            {
                sources = new AudioSources();
            }
            return sources;
        }
    }
}