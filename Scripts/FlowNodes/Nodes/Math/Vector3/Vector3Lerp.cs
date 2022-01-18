using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Ћинейна€ интерпол€ци€
    /// </summary>
    [CreateNodeMenu("Vector3/Lerp", 32)]
    [NodeWidth(150)]
    public class Vector3Lerp : MonoNode
    {
        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3      a;

        [Input(connectionType: ConnectionType.Override), HideLabel]
        public Vector3      b;

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
            return Vector3.Lerp(portA.GetInputValue(a), portB.GetInputValue(b), portT.GetInputValue(t));
        }
    }
}
