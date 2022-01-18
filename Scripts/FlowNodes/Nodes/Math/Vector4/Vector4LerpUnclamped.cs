using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Ћинейна€ интерпол€ци€ без ограниченний t
    /// </summary>
    [CreateNodeMenu("Vector4/LerpUnclamped", 33)]
    [NodeWidth(150)]
    public class Vector4LerpUnclamped : MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector4      a;

        [Input(connectionType: ConnectionType.Override)]
        public Vector4      b;

        [Input(connectionType: ConnectionType.Override)]
        public float        t = 0.0f;

        [Output]
        public float            lerpUnclamped;

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
            return Vector4.LerpUnclamped(portA.GetInputValue(a), portB.GetInputValue(b), portT.GetInputValue(t));
        }
    }
}
