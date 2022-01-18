using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    public abstract class AnimateValue : FlowNodeInOut
    {
        [Inline]
        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton]
        public Flow stop;

        [Output(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton]
        public Flow tick;

        [SerializeField, NodeEnum, HideInNodeInspector]
        private EasingMode      easingMode = EasingMode.Linear;

        public EasingMode EasingMode
        {
            get => easingMode;
            set => easingMode = value;
        }

        private NodePort stopPort;
        private NodePort tickPort;

        public NodePort StopPort => stopPort;
        public NodePort TickPort => tickPort;

        public enum State
        {
            Stopped = 0,
            Started = 1,
        }

        protected override void Init()
        {
            base.Init();

            stopPort = GetInputPort(nameof(stop));
            tickPort = GetOutputPort(nameof(tick));
        }

        public abstract System.Type Type
        {
            get;
        }

    }

    public abstract class AnimateValue<T> : AnimateValue
    {
        [Input(connectionType: ConnectionType.Override), Inline]
        public T from = default(T);

        [Output]
        public T value;

        [Input(connectionType: ConnectionType.Override)]
        public T to = default(T);

        [Input(connectionType: ConnectionType.Override)]
        public float duration = 1;


        public override System.Type Type => typeof(T);      

        public NodePort FromPort  => fromPort;
        public NodePort ToPort => toPort;
        public NodePort DurationPort  => durationPort;

        private NodePort valuePort;
        private NodePort fromPort;
        private NodePort toPort;
        private NodePort durationPort;

        private float remainingSec = 0.0f;

        private State state = State.Stopped;

        protected override void Init()
        {
            base.Init();

            fromPort = GetInputPort(nameof(from));
            toPort = GetInputPort(nameof(to));
            durationPort = GetInputPort(nameof(duration));
            valuePort = GetOutputPort(nameof(value));

            FlowInputPort.label = "Start";
            FlowOutputPort.label = "Completed";

            durationPort.label = "Duration (sec)";
        }

        public override void Flow(NodePort flowPort) 
        {
            if (flowPort == FlowInputPort)
            {
                StartTimer();
            }
            else if (flowPort == StopPort)
            {
                StopTimer();
            }
        }

        public override object GetValue(NodePort port) 
        {
            if (port == valuePort)
            {
                return value;
            }

            return null;
        }

        public override void Stop()
        {
            StopTimer();
        }

        private void Update()
        {
            if (graph.UpdateMode == AnimatorUpdateMode.AnimatePhysics)
                return;

            if (state == State.Started)
            {
                TickTimer();
            }
        }

        private void FixedUpdate()
        {
            if (graph.UpdateMode != AnimatorUpdateMode.AnimatePhysics)
                return;

            if (state != State.Started)
            {
                TickTimer();
            }
        }

        private void StartTimer()
        {
            state = State.Started;
            FlowUtils.FlowOutput(TickPort);
            duration = durationPort.GetInputValue(duration);
            remainingSec = duration;
            from = fromPort.GetInputValue(from);
            to = toPort.GetInputValue(to);
            value = from;
        }

        private void StopTimer()
        {
            if (state == State.Started)
            {
                state = State.Stopped;
            }
        }

        protected abstract T GetValue(float tNormal);

        private void TickTimer()
        {
            remainingSec -= graph.DeltaTime;
            FlowUtils.FlowOutput(TickPort);
            if (remainingSec <= 0.0f || duration <= 0f)
            {
                TimerCompleted();
                return;
            }
            value = GetValue(FloatEase.Ease((duration - remainingSec) / duration, EasingMode));
        }

        private void TimerCompleted()
        {
            value = to;
            remainingSec = 0f;
            state = State.Stopped;
            FlowUtils.FlowOutput(FlowOutputPort);
        }
    }
}
