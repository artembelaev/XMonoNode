using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Ћинейна€ интерпол€ци€ с неограниченным t
    /// </summary>
    [CreateNodeMenu("Float/LerpUnclamped", -165)]
    [NodeWidth(160)]
    //[ExecuteInEditMode]
    public class FloatLerpUnclamped : MonoNode
    {

        [Input(connectionType: ConnectionType.Override)]
        public float            a = 0.0f;

        [Input(connectionType: ConnectionType.Override)]
        public float            b = 1f;

        [Input(connectionType: ConnectionType.Override)]
        public float            t = 0.0f;

        [Output]
        public float            lerpUnclamped;

        private NodePort portA;
        private NodePort portB;
        private NodePort portT;

        private void Reset()
        {
            Name = "Lerp Unclamped";
        }

        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));
            portT = GetInputPort(nameof(t));
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.LerpUnclamped(portA.GetInputValue(a), portB.GetInputValue(b), portT.GetInputValue(t));
        }
    }
}
