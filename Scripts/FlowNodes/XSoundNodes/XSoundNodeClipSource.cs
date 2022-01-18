using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace XMonoNode
{
    /// <summary>
    /// Нода-источник звуков. Может воспроизводить AudioClip
    /// </summary>
    [AddComponentMenu("X Sound Node/ClipSource", 1)]
    [CreateNodeMenu("Sound/ClipSource", 1)]
    [NodeWidth(230)]
    [NodeTint(70, 100, 70)]
    public class XSoundNodeClipSource : XSoundNodeBase
    {
        [Output(ShowBackingValue.Never, ConnectionType.Multiple, TypeConstraint.Inherited)] 
        public AudioSources audioOutput;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public AudioClip clip = null;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public float time = 0f;

        [Header("AudioSource Parameters")]
        [Input(connectionType: ConnectionType.Override), Hiding, HideLabel]
        public AudioMixerGroup mixerGroup = null;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public bool loop = false;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(0f, 1f)]
        public float volume = 1f;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(-3f, 3f)]
        public float pitch = 1f;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(-1f, 1f)]
        public float stereoPan = 0f;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(0f, 1f)]
        public float spatialBlend = 0f;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(0f, 1.1f)]
        public float reverbZoneMix = 1f;

        [Header("3D Sound Settings")]

        [Input(connectionType: ConnectionType.Override), Hiding, Range(0f, 5f)]
        public float dopplerLevel = 1f;

        [Input(connectionType: ConnectionType.Override), Hiding, Range(0f, 360f)]
        public float spread = 1f;

        [Input(connectionType: ConnectionType.Override), Hiding, NodeEnum]
        public AudioRolloffMode volumeRolloff = AudioRolloffMode.Logarithmic;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public float minDistance = 1f;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public float maxDistance = 500f;

        private List<AudioSource> sources = new List<AudioSource>();

        private NodePort clipPort = null;

        private NodePort timePort = null;

        private NodePort mixerGroupPort = null;
        private NodePort loopPort = null;
        private NodePort volumePort = null;
        private NodePort pitchPort = null;
        private NodePort stereoPanPort = null;
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

            clipPort = GetInputPort(nameof(clip));
            timePort = GetInputPort(nameof(time));
            mixerGroupPort = GetInputPort(nameof(mixerGroup));
            loopPort = GetInputPort(nameof(loop));
            volumePort = GetInputPort(nameof(volume));
            pitchPort = GetInputPort(nameof(pitch));
            stereoPanPort = GetInputPort(nameof(stereoPan));
            spatialBlendPort = GetInputPort(nameof(spatialBlend));
            reverbZoneMixPort = GetInputPort(nameof(reverbZoneMix));
            dopplerLevelPort = GetInputPort(nameof(dopplerLevel));
            spreadPort = GetInputPort(nameof(spread));
            volumeRolloffPort = GetInputPort(nameof(volumeRolloff));
            minDistancePort = GetInputPort(nameof(minDistance));
            maxDistancePort = GetInputPort(nameof(maxDistance));
        }

        private void InitSource(AudioSource source)
        {
            source.outputAudioMixerGroup = mixerGroupPort.GetInputValue(mixerGroup);
            source.time         = Mathf.Clamp(timePort.GetInputValue(time), 0f, source.clip.length - 0.001f);
            source.loop         = loopPort.GetInputValue(loop);
            source.volume       = volumePort.GetInputValue(volume);
            source.pitch        = pitchPort.GetInputValue(pitch);
            source.panStereo    = stereoPanPort.GetInputValue(stereoPan);
            source.spatialBlend = spatialBlendPort.GetInputValue(spatialBlend);
            source.reverbZoneMix= reverbZoneMixPort.GetInputValue(reverbZoneMix);
            source.dopplerLevel = dopplerLevelPort.GetInputValue(dopplerLevel);
            source.spread       = spreadPort.GetInputValue(spread);
            source.rolloffMode  = volumeRolloffPort.GetInputValue(volumeRolloff);
            source.minDistance  = minDistancePort.GetInputValue(minDistance);
            source.maxDistance  = maxDistancePort.GetInputValue(maxDistance);
        }

        private void Reset()
        {
            Name = "Clip Audio Source";
        }

        public override object GetValue(NodePort port)
        {
            clip = clipPort.GetInputValue(clip);

            if (clip == null)
            {
                Debug.LogErrorFormat(this, "The audio clip is not defined! {0} ({1})".Color(Color.magenta), gameObject.name, Name);
                return new AudioSources();
            }

            AudioSource source = sources.Find(s => s != null && !s.isPlaying);

            if (source == null)
            {
                source = new GameObject(string.Format("AudioSource: {0}", clip.name)).AddComponent<AudioSource>();
                sources.Add(source);
#if UNITY_EDITOR
                if (Application.isEditor)
                {
                    source.gameObject.hideFlags = HideFlags.DontSave;
                }
#endif
                sources.Add(source);
            }

            source.clip = clip;
            source.Play();

            InitSource(source);

            if (source != null)
            {
                return new AudioSources(new List<AudioSource>() { source });
            }
            else
            {
                return new AudioSources();
            }
        }
    }
}
