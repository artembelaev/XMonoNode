using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    [CreateNodeMenu("Logic/Logic Wait", 103)]
    [NodeWidth(140)]
    public class LogicWait : MonoNode, IUpdatable
    {
        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), Inline, HideLabel]
        public bool input = false;

        [Output, HideLabel]
        public bool output = false;


        [Input(connectionType: ConnectionType.Override, typeConstraint: TypeConstraint.Inherited), HideLabel, Hiding, Inline]
        public float waitSec = 1f;

        [Output, HideLabel, Hiding]
        public float percent = 0f;


        protected NodePort inputPort = null;
        protected NodePort outputPort = null;
        protected NodePort waitSecondsPort = null;
        protected NodePort percentPort = null;
        private bool                            clamped = true;

        private float time = 0f;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(input));
            outputPort = GetOutputPort(nameof(output));
            waitSecondsPort = GetInputPort(nameof(waitSec));
            percentPort = GetOutputPort(nameof(percent));
        }

        public override object GetValue(NodePort port)
        {
            if (port == outputPort)
            {
                return output;
            }
            else //if (port == percentPort)
            {
                return percent;
            }
        }

        public void OnUpdate(float deltaTime)
        {
            bool input = inputPort.GetInputValue(this.input);
            if (waitSec > 0f)
            {
                if (!clamped)
                {
                    if (input)
                    {
                        time += deltaTime;
                        if (time >= waitSec)
                        {
                            time = waitSec;
                            output = true;
                            clamped = true;
                        }
                    }
                    else
                    {
                        time -= deltaTime;
                        if (time <= 0f)
                        {
                            time = 0f;
                            output = false;
                            clamped = true;
                        }
                    }
                    percent = time / waitSec;
                }
                else if (input != output)
                {
                    clamped = false;
                }
   
            }
            else
            {
                percent = input ? 1f : 0f;
                output = input;
            }
            this.input = input;
        }
    }
}