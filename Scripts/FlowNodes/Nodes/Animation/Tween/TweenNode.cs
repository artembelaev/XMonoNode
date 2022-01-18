using UnityEngine;

namespace XMonoNode
{
    public abstract class TweenNode : FlowNodeInOut
    {
        public enum LoopType
        {
            Restart = 0,
            Yoyo = 1,
            Incremental = 2
        }

        public enum State
        {
            Stopped = 0,
            Wait = 1,
            Started = 2,
        }

        public State state => _state;
        private State _state = State.Stopped;

        [Output(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton, Hiding]
        public Flow onStart;

        [Input(connectionType: ConnectionType.Override)]
        public float duration = 1f;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public float delay = 0f;

        [Input(connectionType: ConnectionType.Override), Hiding]
        public int loopsAmount;

        [NodeEnum, Hiding]
        public LoopType loop = LoopType.Restart;

        [NodeEnum]
        public EasingMode easingMode = EasingMode.Linear;

        protected abstract void OnTweenStart();

        protected abstract void OnTweenTick(float tNormal);

        protected abstract void OnTweenEnd();

        protected abstract void OnNextLoop(LoopType loopType);

        private float remainingSec = 0.0f;
        private float waitRemainingSec = 0.0f;
        private int loopsCount = 0;

        private NodePort    onStartPort = null;

        protected override void Init()
        {
            base.Init();
            FlowInputPort.label = "Begin";
            FlowOutputPort.label = "End";
            GetInputPort(nameof(duration)).label = "Duration (sec)";
            GetInputPort(nameof(delay)).label = "Delay (sec)";
            onStartPort = GetOutputPort(nameof(onStart));
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public override void Flow(NodePort flowPort)
        {
            if (flowPort == FlowInputPort && state == State.Stopped)
            {
                StartTimer();
            }
        }

        private void Update()
        {
            if (graph.UpdateMode == AnimatorUpdateMode.AnimatePhysics)
                return;

            if (state != State.Stopped)
            {
                TickTimer();
            }
        }

        private void FixedUpdate()
        {
            if (graph.UpdateMode != AnimatorUpdateMode.AnimatePhysics)
                return;

            if (state != State.Stopped)
            {
                TickTimer();
            }
        }

        private void StartTimer()
        {
            duration = GetInputValue(nameof(duration), duration);

            delay = GetInputValue(nameof(delay), delay);
            loopsAmount = GetInputValue(nameof(loopsAmount), loopsAmount);
            waitRemainingSec = delay;
            remainingSec = duration;
            
            loopsCount = 0;

            if (waitRemainingSec > 0.0f)
            {
                _state = State.Wait;
            }
            else
            {
                _state = State.Started;
                OnTweenStart();
                TickTimer();

                FlowUtils.FlowOutput(onStartPort);
            }
        }

        private void TickTimer()
        {
            if (state == State.Wait)
            {
                waitRemainingSec -= graph.DeltaTime;
                if (waitRemainingSec <= 0.0f)
                {
                    _state = State.Started;
                    remainingSec += waitRemainingSec; // погрешность    
                    OnTweenStart();
                    FlowUtils.FlowOutput(onStartPort);
                }
            }

            if (state == State.Started)
            {
                float time = (duration != 0) ? (duration - remainingSec) / duration : 1.0f; // время [0..1]

                if (loop == LoopType.Yoyo && (loopsCount % 2) == 1) // время в обратную сторону
                {
                    time = 1.0f - time;
                }

                OnTweenTick(FloatEase.Ease(time, easingMode));

                
                if (remainingSec <= 0.0f)
                {
                    ++loopsCount;
                    
                    if (loopsCount == loopsAmount || loopsAmount == 0)  // stop
                    {
                        OnTweenTick(loop == LoopType.Yoyo && (loopsCount % 2) == 0 ? 0f : 1f); // исключаем погрешность - приводим к целевому показателю
                        FlowOut();
                        remainingSec = 0.0f;
                        _state = State.Stopped;
                        OnTweenEnd();
                        return;
                    }
                    else // next loop
                    {
                        remainingSec += duration; // начинаем отсчет сначала 
                        OnNextLoop(loop);
                    }
                }
                remainingSec -= graph.DeltaTime;


            }
        }

        public override void Stop()
        {
            base.Stop();
            _state = State.Stopped;
        }

    }

    public abstract class TweenObjectValue<Obj, Val> : TweenNode where Obj : UnityEngine.Object
    {
        [Input(connectionType: ConnectionType.Override)]
        public Obj target;

        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited)]
        public Val targetValue;

        protected Val startValue;

        protected NodePort targetPort;
        protected NodePort targetValuePort;

        protected override void Init()
        {
            base.Init();
            targetPort = GetInputPort(nameof(target));
            targetValuePort = GetInputPort(nameof(targetValue));
#if UNITY_EDITOR
            try
            {

                targetPort.label = UnityEditor.ObjectNames.NicifyVariableName(typeof(Obj).PrettyName());
                targetValuePort.label = UnityEditor.ObjectNames.NicifyVariableName(typeof(Val).PrettyName());
            }
            catch
            {}
#endif
        }

        private void Reset()
        {
#if UNITY_EDITOR
            Name = $"{UnityEditor.ObjectNames.NicifyVariableName(typeof(Val).PrettyName())} {UnityEditor.ObjectNames.NicifyVariableName(typeof(Obj).PrettyName())}";
#endif

        }

        protected NodePort TargetValuePort  => targetValuePort;

        protected abstract Val GetStartValue();

        protected abstract void SetValue(Val value);

        protected override void OnTweenStart()
        {
            object objTarget = targetPort.GetInputValue();

            if (objTarget is Obj)
            {
                target = objTarget as Obj;
            }
            else if (objTarget is Component)
            {
                target = (objTarget as Component).GetComponent<Obj>();
            }
            else if (objTarget is GameObject)
            {
                target = (objTarget as GameObject).GetComponent<Obj>();
            }

            if (target == null)
            {
                Debug.LogErrorFormat("Tween node target is null ({0}.{1})", gameObject.name, Name);
                return;
            }
            startValue = GetStartValue();
            targetValue = GetInputValue(nameof(targetValue), targetValue);
        }

        protected override void OnTweenEnd()
        {

        }
    }
}
