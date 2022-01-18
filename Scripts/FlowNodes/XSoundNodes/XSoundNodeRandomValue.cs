using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Изменяет свойство случайным образом
    /// </summary>
    public abstract class XSoundNodeRandomValue : XSoundNodeSimpleOutput
    {
        [Input(connectionType: ConnectionType.Override)]
        public float                minValue = 0f;

        [Input(connectionType: ConnectionType.Override)]
        public float                maxValue = 1f;

        protected NodePort          minValuePort;
        protected NodePort          maxValuePort;

        protected override void Init()
        {
            base.Init();

            minValuePort = GetInputPort(nameof(minValue));
            maxValuePort = GetInputPort(nameof(maxValue));
        }

        public override object GetValue(NodePort port)
        {
            minValue = minValuePort.GetInputValue(minValue);
            maxValue = maxValuePort.GetInputValue(maxValue);
            float randomValue = Random.Range(minValue, maxValue);

            AudioSources sources = GetAudioInput();
            setRandomValue(randomValue, sources);
            return sources;
        }

        protected abstract void setRandomValue(float randomValue, AudioSources sources);
    }
}
