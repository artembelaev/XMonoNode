using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Ћинейна€ интерпол€ци€
    /// </summary>
    [CreateNodeMenu("Vector4/Lerp", 32)]
    [NodeWidth(150)]
    public class Vector4Lerp : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4      a;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4      b;

        [Input(connectionType: ConnectionType.Override)]
        public float        t = 0.0f;

        [Output]
        public float            lerp = 0.0f;

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
            return Vector4.Lerp(portA.GetInputValue(a), portB.GetInputValue(b), portT.GetInputValue(t));
        }
    }
}
