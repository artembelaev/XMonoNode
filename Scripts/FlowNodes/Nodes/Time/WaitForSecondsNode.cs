using System.Threading.Tasks;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Time/Wait For Seconds", 532)]
    [NodeWidth(150)]
    public class WaitForSecondsNode : FlowNodeInOut
    {
        [Input(backingValue: ShowBackingValue.Never,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None),
            NodeInspectorButton,
            Hiding]
        public Flow stop;

        [Input] public float WaitSeconds;

        private bool flow = false;

        private NodePort stopPort = null;

        private void Reset()
        {
            Name = "Wait For Seconds";
        }

        protected override void Init()
        {
            base.Init();
            GetInputPort(nameof(FlowInput)).label = "Enter";
            GetOutputPort(nameof(FlowOutput)).label = "Exit";
            stopPort = GetInputPort(nameof(stop));
        }

        public override async void Flow(NodePort flowPort)
        {
            if (flowPort == FlowInputPort)
            {
                var secondsToWait = GetInputValue(nameof(WaitSeconds), WaitSeconds);
                if (secondsToWait >= 0)
                {
                    flow = true;
                    await DoWait((int)(secondsToWait * 1000));
                }
            }
            else if (flowPort == stopPort)
            {
                Stop();
            }
        }

        public async Task DoWait(int waitMilliseconds)
        {
            await Task.Delay(waitMilliseconds);
            if (flow)
            {
                FlowOut();
            }
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }

        public override void Stop()
        {
            base.Stop();
            flow = false;
        }
    }
}
