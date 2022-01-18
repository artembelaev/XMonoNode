using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Reflects a vector of the plane defined by a normal
    /// </summary>
    [CreateNodeMenu("Vector2/Reflect", 38)]
    [NodeWidth(135)]
    public class Vector2Reflect: MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector2      inDirection;

        [Input(connectionType: ConnectionType.Override)]
        public Vector2      inNormal;

        [Output]
        public Vector2      reflect;

        private NodePort portA;
        private NodePort portB;

        protected override void Init()
        {
            base.Init();
            portA = GetInputPort(nameof(inDirection));
            portB = GetInputPort(nameof(inNormal));
        }

        public override object GetValue(NodePort port)
        {
            return Vector2.Reflect(portA.GetInputValue(inDirection), portB.GetInputValue(inNormal));
        }
    }
}
