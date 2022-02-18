using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// ��������� �������������� ���� ���� ��������� ��������
    /// </summary>
    [AddComponentMenu("X Sound Node/Time Access", 102)]
    [CreateNodeMenu("Sound/Time Access", 102)]
    [NodeWidth(130)]
    public class XSoundNodeTimeAccess : XSoundNodeSimpleOutput
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), HideLabel]
        public float            timeAccess = 0.125f;

        private double          lastTime = 0;

        public static DateTime              epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private double CurrentTime()
        {
            // TODO ���������� ��������� �������� ��� � ExtensionsBase.CurrentTime()
            return (DateTime.UtcNow - epochStart).TotalSeconds;
        }


        private void Reset()
        {
            Name = "Time Access";
        }

        public override object GetValue(NodePort port)
        {
            double currentTime = CurrentTime();
            timeAccess = GetInputValue(nameof(timeAccess), timeAccess);

            if (currentTime - lastTime < timeAccess)
            {
                return new AudioSources();
            }

            lastTime = currentTime;

            return GetAudioInput();
        }
    }
}
