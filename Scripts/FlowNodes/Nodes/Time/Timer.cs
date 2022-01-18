using System.Threading.Tasks;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Timer", 538)]
    [NodeWidth(150)]
    public class Timer : MonoNode, IFlowNode
    {
        [Inline]
        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.Strict),
            NodeInspectorButton]
        public Flow start;

        [Output(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.Strict),
            NodeInspectorButton]
        public Flow started;

        [Inline]
        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.Strict),
            NodeInspectorButton]
        public Flow pause;

        [Output(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.Strict),
            NodeInspectorButton]
        public Flow tick;

        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.Strict),
            NodeInspectorButton]
        public Flow resume;

        [Inline]
        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.Strict),
            NodeInspectorButton]
        public Flow stop;

        [Output(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.Strict),
            NodeInspectorButton]
        public Flow completed;

        [Input(backingValue: ShowBackingValue.Unconnected,
            connectionType: ConnectionType.Override)]
        public float duration = 1.0f;

        [Output]
        public float elapsed;

        [Output]
        public float elapsedPercent;

        [Output]
        public float remaining;

        [Output]
        public float remainingPercent;

        private NodePort startPort;
        private NodePort pausePort;
        private NodePort resumePort;
        private NodePort stopPort;
        private NodePort startedPort;
        private NodePort tickPort;
        private NodePort completedPort;

        private NodePort durationPort;
        private NodePort elapsedPort;
        private NodePort elapsedPercentPort;
        private NodePort remainingPort;
        private NodePort remainingPercentPort;

        private float remainingSec = 0.0f;

        private enum TimerState
        {
            Stopped = 0,
            Started = 1,
            Paused = 2,
        }

        private TimerState state = TimerState.Stopped;

        protected override void Init()
        {
            base.Init();

            startPort = GetInputPort(nameof(start));
            pausePort = GetInputPort(nameof(pause));
            resumePort = GetInputPort(nameof(resume));
            stopPort = GetInputPort(nameof(stop));
            startedPort = GetOutputPort(nameof(started));
            tickPort = GetOutputPort(nameof(tick));
            completedPort = GetOutputPort(nameof(completed));

            durationPort = GetInputPort(nameof(duration));
            elapsedPort = GetOutputPort(nameof(elapsed));
            elapsedPercentPort = GetOutputPort(nameof(elapsedPercent));
            remainingPort = GetOutputPort(nameof(remaining));
            remainingPercentPort = GetOutputPort(nameof(remainingPercent));

            durationPort.label = "Duration (sec)";

            elapsedPercentPort.label = "Elapsed %";
            remainingPercentPort.label = "Remaining %";
        }

        public void TriggerFlow()
        {
            //base.TriggerFlow(); 
        }
   
        public void Flow(NodePort flowPort)
        {
            if (flowPort == startPort)
            {
                StartTimer();
            }
            else if (flowPort == pausePort)
            {
                PauseTimer();
            }
            else if (flowPort == resumePort)
            {
                ResumeTimer();
            }
            else if (flowPort == stopPort)
            {
                StopTimer();
            }
        }

        public virtual void Stop()
        {
            StopTimer();
        }

        private void Update()
        {
            if (graph.UpdateMode == AnimatorUpdateMode.AnimatePhysics)
                return;

            if (state == TimerState.Started)
            {
                TickTimer();
            }
        }

        private void FixedUpdate()
        {
            if (graph.UpdateMode != AnimatorUpdateMode.AnimatePhysics)
                return;

            if (state == TimerState.Started)
            {
                TickTimer();
            }
        }

        public override object GetValue(NodePort port)
        {
            if (port == elapsedPort)
            {
                return duration - remainingSec;
            }
            else if (port == elapsedPercentPort)
            {
                return (duration - remainingSec) / duration;
            }
            else if (port == remainingPort)
            {
                return remainingSec;
            }
            else if (port == remainingPercentPort)
            {
                return remainingSec / duration;
            }


            return null;
        }

        private void StartTimer()
        {
            state = TimerState.Started;
            FlowUtils.FlowOutput(startedPort);
            duration = durationPort.GetInputValue(duration);
            remainingSec = duration;
        }

        private void PauseTimer()
        {
            if (state == TimerState.Started)
            {
                state = TimerState.Paused;
                FlowUtils.FlowOutput(startedPort);
            }
        }

        private void ResumeTimer()
        {
            if (state == TimerState.Paused)
            {
                state = TimerState.Started;
            }
        }

        private void StopTimer()
        {
            if (state == TimerState.Started || state == TimerState.Paused)
            {
                state = TimerState.Stopped;
            }
        }
        private void TickTimer()
        {
            remainingSec -= graph.DeltaTime;
            FlowUtils.FlowOutput(tickPort);
            if (remainingSec <= 0.0f)
            {
                TimerCompleted();
            }
        }

        private void TimerCompleted()
        {
            state = TimerState.Stopped;
            FlowUtils.FlowOutput(completedPort);
        }
    }
}
