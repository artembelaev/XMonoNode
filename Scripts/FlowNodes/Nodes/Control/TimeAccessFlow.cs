using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// «апрещает воспроизводить звук чаще заданного значени€
    /// </summary>
    [AddComponentMenu("Control/Time Access", 24)]
    [CreateNodeMenu("Control/Time Access", 24)]
    [NodeWidth(210)]
    public class TimeAccessFlow : FlowNodeInOut
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Inline]
        public float            minDeltaTime = 0.125f;

        [Output, Hiding]
        public Flow             lessDelta;

        private NodePort        minDeltaTimePort = null;
        private NodePort        lessDeltaPort = null;

        private double          lastTime = 0;

        public static DateTime              epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private double CurrentTime()
        {
            // TODO кешировать результат подсчета как в ExtensionsBase.CurrentTime()
            return (DateTime.UtcNow - epochStart).TotalSeconds;
        }

        private void Reset()
        {
            Name = "Time Access";
        }

        protected override void Init()
        {
            base.Init();

            minDeltaTimePort = GetInputPort(nameof(minDeltaTime));
            lessDeltaPort = GetOutputPort(nameof(lessDelta));

            minDeltaTimePort.label = "Min dt";
            FlowOutputPort.label = "dt >= min";
            lessDeltaPort.label = "dt < min";

        }

        public override void Flow(NodePort flowPort)
        {
            double currentTime = CurrentTime();
            minDeltaTime = GetInputValue(nameof(minDeltaTime), minDeltaTime);

            bool less = currentTime - lastTime < minDeltaTime;

            lastTime = currentTime;

            FlowUtils.FlowOutput(less ? lessDeltaPort : FlowOutputPort);
        }

        public override object GetValue(NodePort port)
        {
            throw null;
        }
    }
}
