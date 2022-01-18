using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Ћинейна€ интерпол€ци€
    /// </summary>
    [CreateNodeMenu("Float/Lerp", -167)]
    [NodeWidth(160)]
    //[ExecuteInEditMode]
    public class FloatLerp : MonoNode
    {

        [Input(connectionType: ConnectionType.Override)]
        public float            a = 0.0f;

        [Input(connectionType: ConnectionType.Override)]
        public float            b = 1.0f;

        [Input(connectionType: ConnectionType.Override)]
        public float            t = 0.0f;

        [Output]
        public float            lerp;

        private NodePort portA;
        private NodePort portB;
        private NodePort portT;

        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(a));
            portB = GetInputPort(nameof(b));
            portT = GetInputPort(nameof(t));
        }

        public override object GetValue(NodePort port)
        {
            return Mathf.Lerp(portA.GetInputValue(a), portB.GetInputValue(b), portT.GetInputValue(t));
        }
    }
}
