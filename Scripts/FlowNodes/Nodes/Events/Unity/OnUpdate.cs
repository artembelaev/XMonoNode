using UnityEngine;

namespace XMonoNode
{
    [CreateNodeMenu("Events/Lifecicle/" + nameof(OnUpdate), 15)]
    //[ExecuteInEditMode]
    [NodeWidth(150)]
    public class OnUpdate : EventNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public int Milliseconds;

        protected NodePort MillisecondsPort = null;

        protected override void Init()
        {
            base.Init();

            MillisecondsPort = GetInputPort(nameof(Milliseconds));
        }

        private float _timestamp
        {
            get; set;
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup > _timestamp)
            {
                TriggerFlow();
                Milliseconds = MillisecondsPort.GetInputValue(Milliseconds);
                _timestamp = Time.realtimeSinceStartup + Milliseconds * 0.001f;
            }
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }
    }
}
