﻿using UnityEngine;
using System.Collections.Generic;

namespace XMonoNode
{
    /// <summary>
    /// Fades the sound
    /// </summary>
    [AddComponentMenu("X Sound Node/Fade Out", 311)]
    [CreateNodeMenu("Sound/Fade Out", 311)]
    [NodeWidth(180)]
    public class XSoundNodeFadeOut : FlowNodeInOut
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public AudioSources input = new AudioSources();

        [Output, NodeInspectorButton]
        public Flow completed;

        [SerializeField, NodeEnum, HideInNodeInspector]
        private EasingMode      easingMode = EasingMode.Linear;

        [Input(connectionType: ConnectionType.Override), Range(0f, 60f)]
        public float duration = 1f;

        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton, Hiding]
        public Flow stop;

        public EasingMode EasingMode
        {
            get => easingMode;
            set => easingMode = value;
        }

        public XSoundNodeGraph SoundGraph => graph as XSoundNodeGraph;

        public NodePort StopPort => stopPort;
        public NodePort CompletedPort => completedPort;

        public NodePort DurationPort => durationPort;

        public enum State
        {
            Stopped = 0,
            Started = 1,
        }

        private AudioSources playing = new AudioSources();

        private NodePort stopPort;
        private NodePort completedPort;
        private NodePort durationPort;
        private float remainingSec = 0.0f;

        private State state = State.Stopped;

        private Dictionary<AudioSource, float> initialVolume = new Dictionary<AudioSource, float>();

        private void Reset()
        {
            Name = "Fade Out";
        }

        protected override void Init()
        {
            base.Init();

            stopPort = GetInputPort(nameof(stop));
            completedPort = GetOutputPort(nameof(completed));
            durationPort = GetInputPort(nameof(duration));

            FlowInputPort.label = "Start";
            FlowOutputPort.label = "Started";

            durationPort.label = "Duration (sec)";
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void Flow(NodePort flowPort)
        {
            if (flowPort == FlowInputPort)
            {
                StartTimer();
            }
            else if (flowPort == StopPort)
            {
                Stop();
            }
        }
        public override void Stop()
        {
            if (state == State.Started)
            {
                remainingSec = 0f;
                state = State.Stopped;
                playing.Stop();
                playing.List.Clear();
                initialVolume.Clear();
            }
        }

        public override void ConditionalUpdate()
        {
            if (state == State.Started)
            {
                TickTimer();
            }
        }

        private void StartTimer()
        {
            AudioSources sources = GetInputValue<AudioSources>(nameof(input));
            playing.List.AddRange(sources.List);

            state = State.Started;
            duration = durationPort.GetInputValue(duration);
            remainingSec = duration;

            foreach (AudioSource source in playing.List)
            {
                if (source != null)
                {
                    initialVolume[source] = source.volume;
                }
            }
            FlowOut(); // Started

            if (Mathf.Approximately(duration, 0f) || duration < 0f)
            {
                TimerCompleted();
            }
        }

        private void TickTimer()
        {
            remainingSec -= graph.DeltaTime;
            if (remainingSec <= 0.0f || duration <= 0f)
            {
                TimerCompleted();
                return;
            }

            float fade = FloatEase.Ease(remainingSec / duration, EasingMode);
            foreach (AudioSource source in playing.List)
            {
                if (source != null && source.isPlaying)
                {
                    source.volume = fade * initialVolume[source];
                    if (source.volume <= 0f)
                    {
                        source.Stop();
                    }
                }
            }
        }

        private void TimerCompleted()
        {
            Stop();
            FlowUtils.FlowOutput(completedPort);
        }
    }

}
