using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Плеер. Обязательная конечная нода, отвечающая за проигрывание звуков
    /// </summary>
    [AddComponentMenu("X Sound Node/Play", 5)]
    [CreateNodeMenu("Sound/Play", 5)]
    [NodeTint(105, 65, 65)]
    [NodeWidth(150)]
   // [ExecuteInEditMode]
    public class XSoundNodePlay : FlowNodeInOut
    {
        [Inline]
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited, backingValue: ShowBackingValue.Never)]
        public AudioSources audioInput = new AudioSources();
        [Output(typeConstraint: TypeConstraint.Inherited)]
        public AudioSources Playing;
        
        [Inline]
        [Input(backingValue: ShowBackingValue.Never), NodeInspectorButton, Hiding] public Flow stop;
        [Output, NodeInspectorButton, Hiding] public Flow onEnd;

        [Output, NodeInspectorButton, Hiding] public Flow whilePlay;

        public XSoundNodeGraph SoundGraph => graph as XSoundNodeGraph;

        public object[] PlayParameters => SoundGraph?.FlowParametersArray;

        private AudioSources playing;

        // Вспомогательный признак, позволяющий определить, что нода запустила сама себя
        private bool playingState = false;


        protected NodePort audioInputPort = null;
        protected NodePort stopPort;
        protected NodePort playingPort;
        protected NodePort wnilePlayPort;
        protected NodePort onEndPort;

        public AudioSources PlayingSources()
        {
            return playing;
        }

        public override void Flow(NodePort flowPort)
        {
            if (flowPort == FlowInputPort)
            {
                Play(PlayParameters);
            }
            else if (flowPort == stopPort)
            {
                Stop();
            }
        }

        private void Reset()
        {
            Name = "Play";
        }

        public XSoundNodePlay()
        {
            playing = new AudioSources();
        }

        protected override void Init()
        {
            base.Init();

            // Для удобства изменим подпись к стандартным flow портам

            FlowInputPort.label = "Play";
            FlowOutputPort.label = "On Start";

            stopPort = GetInputPort(nameof(stop));
            playingPort = GetOutputPort(nameof(Playing));
            wnilePlayPort = GetOutputPort(nameof(whilePlay));
            onEndPort = GetOutputPort(nameof(onEnd));
            audioInputPort = GetInputPort(nameof(audioInput));

            audioInputPort.label = "Input";
            
        }

        private void Update()
        {
            if (SourcesIsPlaying) // зук проигрывается сейчас
            {
                TriggerWhilePlay();
            }
            else if (playing.Count != 0) // звук завершился
            {
                playingState = false;

                if (Application.isFocused)
                {
                    TriggerOnEnd();
                }

                if (SourcesIsPlaying == false) // не даем удалить запущенные по цепочке звуки (если следующая нода взяла звуки этой ноды)
                {
                    if (Application.isPlaying == false)
                    {
                        playing.DestroySourcesIfStopped();
                    }
                }
                if (!playingState && Application.isFocused) // нельзя удалять, если приложение не в фокусе - звуки могут быть приостановлены
                {
                    playing.Clear(); 
                }
            }
        }

        public void Play(params object[] parameters)
        {
            playingState = true;
            
            if (SoundGraph != null)
            {
                SoundGraph.FlowParametersArray = parameters;
            }

            AudioSources sources = audioInputPort.GetInputValue<AudioSources>();
            if (sources == null) // только нуллы - ошибка. Пустой контейнер (sources.List.Count == 0) - это нормально
            {
                Debug.LogErrorFormat(this, "An audio source is not attached to the Play node {0} ({1})".Color(Color.magenta), gameObject.name, Name);
                return;
            }

            foreach (AudioSource source in sources.List)
            {
                if (source == null)
                {
                    continue;
                }

                source.Play();
            }
            
            if (Application.isPlaying == false)
            {// В режиме редактора мы удаляем не играющие звуки
                playing.List.ForEach(s => { if (s != null && !s.isPlaying) DestroyImmediate(s.gameObject); });
            }

            // Удаляем все, кроме зацикленных
            playing.List.RemoveAll(s => s == null || (!s.loop && !s.isPlaying));
            
            playing.List.AddRange(sources.List);

            TriggerOnStart();
            TriggerWhilePlay();
        }

        [ContextMenu("Play")]
        public void TestPlay()
        {
            FlowNodeGraph flowGraph = graph as FlowNodeGraph;
            if (flowGraph != null)
            {
                if (Application.isPlaying == false)
                {
                    flowGraph.Stop();
                }
                flowGraph.UpdateTestParameters();
                Play(flowGraph.FlowParametersArray);
            }
        }

        [ContextMenu("Stop")]
        public override void Stop()
        {
            playingState = false;
            playing.Stop();
            if (Application.isPlaying == false)
            {
                playing.DestroySourcesIfStopped();
            }
            //playing.DestroySourcesIfLoop();
            playing.List.Clear();
        }

        public bool SourcesIsPlaying => playing.IsPlaying;


        public override object GetValue(NodePort port)
        {
            if (port == playingPort)
            {
                return PlayingSources();
            }
            else
            {
                return null;
            }
        }

        private void TriggerOnStart()
        {
            FlowUtils.FlowOutput(FlowOutputPort);
        }

        private void TriggerWhilePlay()
        {
            FlowUtils.FlowOutput(wnilePlayPort);
        }

        private void TriggerOnEnd()
        {
            FlowUtils.FlowOutput(onEndPort);
        }

    }
}
