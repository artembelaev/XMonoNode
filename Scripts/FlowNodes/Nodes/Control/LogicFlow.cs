using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Flow when input changes from false to true
    /// </summary>
    [AddComponentMenu("Control/Logic Flow", 25)]
    [CreateNodeMenu("Control/Logic Flow", 25)]
    [NodeWidth(130)]
    public class LogicFlow : MonoNode, IFlowNode, IUpdatable
    {
        [Input(connectionType: ConnectionType.Override, backingValue: ShowBackingValue.Never, typeConstraint: TypeConstraint.Inherited), HideLabel, Inline]
        public bool                             input;

        [Output(backingValue: ShowBackingValue.Unconnected,
            connectionType: ConnectionType.Multiple,
            typeConstraint: TypeConstraint.None), NodeInspectorButton, HideLabel]
        public Flow                             Exit;

        [SerializeField, Hiding, Tooltip("Good for optimization")]
        private bool                            oneShot = false; 

        private NodePort                        exitPort = null;
        private NodePort                        inputPort = null;
        private bool                            hasOneShot = false;

        protected override void Init()
        {
            base.Init();

            inputPort = GetInputPort(nameof(input));
            exitPort = GetOutputPort(nameof(Exit));
        }

        public void TriggerFlow()
        {
            FlowUtils.FlowOutput(exitPort);
        }

        /// <summary>
        /// Handle input stream
        /// </summary>
        public void Flow(NodePort flowPort)
        {
            FlowUtils.FlowOutput(exitPort);
        }

        /// <summary>
        /// Stop execution of this flow node
        /// </summary>
        public virtual void Stop()
        {
            hasOneShot = false;
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public void OnUpdate(float deltaTime)
        {
            if (oneShot && hasOneShot)
            {
                return;
            }

            bool inputNew = inputPort.GetInputValue(input);
            if (inputNew && !input) // 0 -> 1
            {
                TriggerFlow();
                hasOneShot = true;
            }
            input = inputNew;
        }
    }
}
