using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    /// <summary>
    /// Изменяет свойство volume
    /// </summary>
    [AddComponentMenu("X Sound Node/Audio Parameters", 50)]
    [CreateNodeMenu("Sound/Audio Parameters", 50)]
    [NodeWidth(160)]
    public class XSoundNodeAudioSourceParams : FlowNodeInOut
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public AudioSources input;

        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)]
        public AudioSources output;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public AudioMixerGroup mixerGroup = null;

        [Input(connectionType: ConnectionType.Override)]
        public bool loop = false;

        [Input(connectionType: ConnectionType.Override), Range(0f, 1f)]
        public float volume = 1f;

        [Input(connectionType: ConnectionType.Override), Range(-3f, 3f)]
        public float pitch = 1f;

        [Input(connectionType: ConnectionType.Override), Range(-1f, 1f)]
        public float stereoPan = 0f;

        [Input(connectionType: ConnectionType.Override), Range(0f, 1f)]
        public float spatialBlend = 0f;

        private NodePort audioInputPort = null;
        private NodePort audioOutputPort = null;

        private NodePort mixerGroupPort = null;
        private NodePort loopPort = null;
        private NodePort volumePort = null;
        private NodePort pitchPort = null;
        private NodePort stereoPanPort = null;
        private NodePort spatialBlendPort = null;
        

        protected override void Init()
        {
            base.Init();

            audioInputPort = GetInputPort(nameof(input));
            audioOutputPort = GetOutputPort(nameof(output));

            mixerGroupPort = GetInputPort(nameof(mixerGroup));
            loopPort = GetInputPort(nameof(loop));
            volumePort = GetInputPort(nameof(volume));
            pitchPort = GetInputPort(nameof(pitch));
            stereoPanPort = GetInputPort(nameof(stereoPan));
            spatialBlendPort = GetInputPort(nameof(spatialBlend));
            
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
            Name = "Audio Parameters";
        }

        public override void Flow(NodePort flowPort)
        {
            changeParams();
            FlowOut();
        }

        public override object GetValue(NodePort port)
        {
            if (port == audioOutputPort)
            {
                return changeParams();
            }
            else
                return null;
        }

        private object changeParams()
        {
            AudioSources sources = GetAudioInput();
            foreach (AudioSource source in sources.List)
            {
                if (source == null)
                    continue;

                source.outputAudioMixerGroup = mixerGroupPort.GetInputValue(mixerGroup);
                source.loop = loopPort.GetInputValue(loop);
                source.volume = volumePort.GetInputValue(volume);
                source.pitch = pitchPort.GetInputValue(pitch);
                source.panStereo = stereoPanPort.GetInputValue(stereoPan);
                source.spatialBlend = spatialBlendPort.GetInputValue(spatialBlend);
            }
            return sources;
        }
    }
}
