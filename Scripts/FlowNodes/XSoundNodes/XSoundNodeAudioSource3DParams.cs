using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    /// <summary>
    /// Изменяет свойство volume
    /// </summary>
    [AddComponentMenu("X Sound Node/Audio 3D Parameters", 51)]
    [CreateNodeMenu("Sound/Audio 3D Parameters", 51)]
    [NodeWidth(240)]
    public class XSoundNodeAudioSource3DParams : FlowNodeInOut
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public AudioSources input;

        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.Inherited)]
        public AudioSources output;

        [Input(connectionType: ConnectionType.Override), Range(0f, 1f)]
        public float spatialBlend = 1f;

        [Input(connectionType: ConnectionType.Override), Range(0f, 1.1f)]
        public float reverbZoneMix = 1f;

        [Input(connectionType: ConnectionType.Override), Range(0f, 5f)]
        public float dopplerLevel = 1f;

        [Input(connectionType: ConnectionType.Override), Range(0f, 360f)]
        public float spread = 1f;

        [Input(connectionType: ConnectionType.Override), NodeEnum]
        public AudioRolloffMode volumeRolloff = AudioRolloffMode.Logarithmic;

        [Input(connectionType: ConnectionType.Override)]
        public float minDistance = 1f;

        [Input(connectionType: ConnectionType.Override)]
        public float maxDistance = 500f;

        private NodePort audioInputPort = null;
        private NodePort audioOutputPort = null;

        private NodePort spatialBlendPort = null;
        private NodePort reverbZoneMixPort = null;
        private NodePort dopplerLevelPort = null;
        private NodePort spreadPort = null;
        private NodePort volumeRolloffPort = null;
        private NodePort minDistancePort = null;
        private NodePort maxDistancePort = null;

        protected override void Init()
        {
            base.Init();

            audioInputPort = GetInputPort(nameof(input));
            audioOutputPort = GetOutputPort(nameof(output));

            spatialBlendPort = GetInputPort(nameof(spatialBlend));
            reverbZoneMixPort = GetInputPort(nameof(reverbZoneMix));
            dopplerLevelPort = GetInputPort(nameof(dopplerLevel));
            spreadPort = GetInputPort(nameof(spread));
            volumeRolloffPort = GetInputPort(nameof(volumeRolloff));
            minDistancePort = GetInputPort(nameof(minDistance));
            maxDistancePort = GetInputPort(nameof(maxDistance));

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
            Name = "Audio 3D Parameters";
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

                source.spatialBlend = spatialBlendPort.GetInputValue(spatialBlend);
                source.reverbZoneMix = reverbZoneMixPort.GetInputValue(reverbZoneMix);
                source.dopplerLevel = dopplerLevelPort.GetInputValue(dopplerLevel);
                source.spread = spreadPort.GetInputValue(spread);
                source.rolloffMode = volumeRolloffPort.GetInputValue(volumeRolloff);
                source.minDistance = minDistancePort.GetInputValue(minDistance);
                source.maxDistance = maxDistancePort.GetInputValue(maxDistance);
            }
            return sources;
        }
    }
}
