using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMonoNode;

namespace XMonoNode
{
    /// <summary>
    /// Reflects a vector of the plane defined by a normal
    /// </summary>
    [CreateNodeMenu("Vector3/Reflect", 38)]
    [NodeWidth(180)]
    public class Vector3Reflect: MonoNode
    {
        [Input(connectionType: ConnectionType.Override)]
        public Vector3      inDirection;

        [Input(connectionType: ConnectionType.Override)]
        public Vector3      inNormal;

        [Output]
        public Vector3      reflect;

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
            return Vector3.Reflect(portA.GetInputValue(inDirection), portB.GetInputValue(inNormal));
        }
    }
}
