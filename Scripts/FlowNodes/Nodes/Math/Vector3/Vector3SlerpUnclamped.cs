using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Ћинейна€ сферическа€ интерпол€ци€ без ограничений t
    /// </summary>
    [CreateNodeMenu("Vector3/SlerpUnclamped", 35)]
    [NodeWidth(150)]
    public class Vector3SlerpUnclamped : MonoNode
    {

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3      a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3      b;

        [Input(connectionType: ConnectionType.Override)]
        public float        t = 0.0f;

        [Output]
        public float            slerpUnclamped;

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
            return Vector3.SlerpUnclamped(portA.GetInputValue(a), portB.GetInputValue(b), portT.GetInputValue(t));
        }
    }
}
